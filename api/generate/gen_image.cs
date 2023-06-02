using abelkhan;
using service;
using StackExchange.Redis;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.Data.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace generate
{
    class gen_image
    {
        private readonly redis_handle _redis_handle;
        private readonly Task work_task;
        private bool run = true;

        private class ImageDBException : System.Exception
        {
            public string err_info;
            public ImageDBException(string err)
            {
                err_info = err;
            }
        }

        private hub.dbproxyproxy.Collection ImageGuidCollection
        {
            get
            {
                return hub.hub.get_random_dbproxyproxy().getCollection(constant.constant.PornoDBName, constant.constant.ImageGuidCollection);
            }
        }

        private Task<long> get_image_guid()
        {
            var task = new TaskCompletionSource<long>();

            ImageGuidCollection.getGuid(constant.constant.InsideGuid, (_result, guid) =>
            {
                if (_result == hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                {
                    task.SetResult(guid);
                }
                else
                {
                    task.SetException(new ImageDBException($"get image guid err:{_result}"));
                }
            });

            return task.Task;
        }

        private async void del_image(string account, string order_uuid, string image_path)
        {
            try
            {
                var lock_key = redis_help.BuildUserOrderCacheLock(account);
                var token = Guid.NewGuid().ToString();
                await _redis_handle.Lock(lock_key, token, 3000);
                var order_list = await _redis_handle.GetData<List<user_order_info>>(redis_help.BuildUserOrderCacheKey(account));
                if (order_list != null)
                {
                    foreach (var order in order_list)
                    {
                        if (order.order_uuid == order_uuid)
                        {
                            order_list.Remove(order);
                            break;
                        }
                    }
                    await _redis_handle.SetData(redis_help.BuildUserOrderCacheKey(account), order_list);
                }
                await _redis_handle.UnLock(lock_key, token);

                if (File.Exists(image_path))
                {
                    File.Delete(image_path);
                }
            }
            catch(System.Exception ex)
            {
                log.log.err("del_image ex:{0}", ex);
            }
        }

        public gen_image(redis_handle _redis_handle_)
        {
            _redis_handle = _redis_handle_;

            work_task = new Task(async () => {
                
                while (run)
                {
                    try
                    {
                        var _order = await _redis_handle.RPopData<order>(constant.constant.GenImageOrderQueue);
                        if (_order == null)
                        {
                            await Task.Delay(33);
                            continue;
                        }

                        var img_guid = await get_image_guid();
                        var outfile = $"image_{img_guid}";

                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.UseShellExecute = false;
                        psi.FileName = "python";
                        psi.Arguments = string.Format("../stablediffusion/scripts/txt2img.py " +
                            "--prompt \"{0}\" --ckpt {1} --outdir {2} --outfile {3} --watermark {4}" +
                            "--config ../stablediffusion/configs/stable-diffusion/v2-inference-v.yaml " +
                            "--H 768 --W 768",
                            string.Join(' ',_order.prompt), constant.constant.SDModelPath, 
                            constant.constant.SDOutputDir, outfile, constant.constant.WterMark);

                        using var p = Process.Start(psi);
                        p.WaitForExit();
                        int ret = p.ExitCode;
                        if (ret != 0)
                        {
                            log.log.err("process error:{0}", ret);
                            continue;
                        }

                        var lock_key = redis_help.BuildUserOrderCacheLock(_order.account);
                        var token = Guid.NewGuid().ToString();
                        await _redis_handle.Lock(lock_key, token, 3000);
                        var order_list = await _redis_handle.GetData<List<user_order_info>>(redis_help.BuildUserOrderCacheKey(_order.account));
                        if (order_list != null)
                        {
                            foreach (var order in order_list)
                            {
                                if (order.order_uuid == _order.order_uuid)
                                {
                                    order.state = gen_image_state.done_generate;
                                    order.url = $"{constant.constant.ImageUrl}{outfile}";
                                    order.img_guid = img_guid;
                                    order.file = Path.Combine(constant.constant.SDOutputDir, outfile);
                                    order.gen_svr = hub.hub.name;
                                }
                            }
                            await _redis_handle.SetData(redis_help.BuildUserOrderCacheKey(_order.account), order_list);
                        }
                        await _redis_handle.UnLock(lock_key, token);

                        hub.hub._timer.addticktime(constant.constant.DelTmpImageTimeTick, (tick) => {
                            del_image(_order.account, _order.order_uuid, Path.Combine(constant.constant.SDOutputDir, outfile));
                        });
                    }
                    catch(System.Exception ex)
                    {
                        log.log.err("gen image error:{0}", ex);
                    }
                };


            }, TaskCreationOptions.LongRunning);
        }

        public void start()
        {
            work_task.Start();
        }

        public async void close()
        {
            run = false;
            await work_task;
        }
    }
}
