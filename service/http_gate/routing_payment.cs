using abelkhan;
using constant;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MsgPack;
using MsgPack.Serialization;
using service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace gate
{
    class routing_payment
    {
        private readonly MessagePackSerializer<free_order_req> _serializer_free_order_req = MessagePackSerializer.Get<free_order_req>();
        private readonly MessagePackSerializer<payment_order_req> _serializer_payment_order_req = MessagePackSerializer.Get<payment_order_req>();
        private readonly MessagePackSerializer<user_payment_req> _serializer_user_payment_req = MessagePackSerializer.Get<user_payment_req>();
        private readonly MessagePackSerializer<user_query_req> _serializer_user_query_req = MessagePackSerializer.Get<user_query_req>();
        private readonly MessagePackSerializer<user_save_req> _serializer_user_save_req = MessagePackSerializer.Get<user_save_req>();
        private readonly MessagePackSerializer<MessagePackObjectDictionary> _serializer_user_payment_rsp = MessagePackSerializer.Get<MessagePackObjectDictionary>();
        private readonly MessagePackSerializer<MessagePackObjectDictionary> _serializer_user_save_rsp = MessagePackSerializer.Get<MessagePackObjectDictionary>();

        private hub.dbproxyproxy.Collection ImageCollection
        {
            get
            {
                return hub.hub.get_random_dbproxyproxy().getCollection(constant.constant.PornoDBName, constant.constant.ImageDBCollection);
            }
        }

        private hub.dbproxyproxy.Collection HotspotCollection
        {
            get
            {
                return hub.hub.get_random_dbproxyproxy().getCollection(constant.constant.PornoDBName, constant.constant.HotspotDBCollection);
            }
        }

        private user_order_info get_and_del_order(string order_uuid, List<user_order_info> order_list)
        {
            foreach (var order in order_list)
            {
                if (order.order_uuid == order_uuid)
                {
                    order_list.Remove(order);
                    return order;
                }
            }
            return null;
        }

        private class EssayDBException : System.Exception
        {
            public string err_info;
            public EssayDBException(string err)
            {
                err_info = err;
            }
        }

        private hub.dbproxyproxy.Collection EssayGuidCollection
        {
            get
            {
                return hub.hub.get_random_dbproxyproxy().getCollection(constant.constant.PornoDBName, constant.constant.EssayGuidCollection);
            }
        }

        private Task<long> get_image_guid()
        {
            var task = new TaskCompletionSource<long>();

            EssayGuidCollection.getGuid(constant.constant.InsideGuid, (_result, guid) =>
            {
                if (_result == hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                {
                    task.SetResult(guid);
                }
                else
                {
                    task.SetException(new EssayDBException($"get image guid err:{_result}"));
                }
            });

            return task.Task;
        }

        public routing_payment()
        {
            HttpService.post("/free_order", free_order);
            HttpService.post("/payment_order", payment_order);
            HttpService.post("/payment_quicken", payment_quicken);
            HttpService.post("/query", query);
            HttpService.post("/save", save_img);
        }

        private async Task save_img(AbelkhanHttpRequest req)
        {
            try
            {
                using var st = MemoryStreamPool.mstMgr.GetStream();
                st.Write(req.Content);
                st.Position = 0;
                var _req = _serializer_user_save_req.Unpack(st);

                var rsp_list = new List<user_payment_info>();

                var user_account = await gate._redis_handle.GetStrData(redis_help.BuildUserTokenAccountKey(_req.token));
                var name = await gate._redis_handle.GetStrData(redis_help.BuildUserAccountNameKey(user_account));

                var rsp = new user_save_rsp()
                {
                    err_code = error.success
                };

                do
                {
                    var lock_key = redis_help.BuildUserOrderCacheLock(user_account);
                    var token = Guid.NewGuid().ToString();
                    await gate._redis_handle.Lock(lock_key, token, 3000);
                    var order_list = await gate._redis_handle.GetData<List<user_order_info>>(redis_help.BuildUserOrderCacheKey(user_account));
                    if (order_list == null)
                    {
                        rsp.err_code = error.img_is_expired;
                        break;
                    }

                    var data = new user_img_db()
                    {
                        account = user_account,
                        create_time = (ulong)timerservice.Tick,
                        essay_guid = await get_image_guid(),
                        free_essay = new List<essay_block>(),
                        pay_essay = new List<essay_block>(),
                        save_state = _req.save_state,
                        prompt = new List<string>(),
                        hotspot = 0,
                    };

                    foreach (var block in _req.free_essay)
                    {
                        var order = get_and_del_order(block.order_uuid, order_list);
                        if (order == null)
                        {
                            rsp.err_code = error.img_is_expired;
                            continue;
                        }
                        var url = await gen_proxy.save_img(order.gen_svr, user_account, order.order_uuid, _req.save_state);
                        if (string.IsNullOrEmpty(url))
                        {
                            rsp.err_code = error.img_is_expired;
                            continue;
                        }


                        if (rsp.err_code == error.success)
                        {
                            data.free_essay.Add(new essay_block()
                            {
                                img_guid = order.img_guid,
                                img_url = order.url,
                                essay = block.essay,
                            });

                            foreach (var prompt in order.prompt)
                            {
                                if (!data.prompt.Contains(prompt))
                                {
                                    data.prompt.Add(prompt);
                                }

                                var query = new DBQueryHelper();
                                query.condition("prompt", prompt);
                                var update = new UpdateDataHelper();
                                update.set("prompt", prompt);
                                update.inc("count", 1);
                                HotspotCollection.updataPersistedObject(query.query(), update.data(), true, (result) =>
                                {
                                    if (result != hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                                    {
                                        log.log.err("save img count prompt error:{0}", result.ToString());
                                    }
                                });
                            }
                        }
                    }

                    foreach (var block in _req.pay_essay)
                    {
                        var order = get_and_del_order(block.order_uuid, order_list);
                        if (order == null)
                        {
                            rsp.err_code = error.img_is_expired;
                            continue;
                        }
                        var url = await gen_proxy.save_img(order.gen_svr, user_account, order.order_uuid, _req.save_state);
                        if (string.IsNullOrEmpty(url))
                        {
                            rsp.err_code = error.img_is_expired;
                            continue;
                        }

                        if (rsp.err_code == error.success)
                        {
                            data.pay_essay.Add(new essay_block()
                            {
                                img_guid = order.img_guid,
                                img_url = order.url,
                                essay = block.essay,
                            });

                            foreach (var prompt in order.prompt)
                            {
                                if (!data.prompt.Contains(prompt))
                                {
                                    data.prompt.Add(prompt);
                                }

                                var query = new DBQueryHelper();
                                query.condition("prompt", prompt);
                                var update = new UpdateDataHelper();
                                update.set("prompt", prompt);
                                update.inc("count", 1);
                                HotspotCollection.updataPersistedObject(query.query(), update.data(), true, (result) =>
                                {
                                    if (result != hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                                    {
                                        log.log.err("save img count prompt error:{0}", result.ToString());
                                    }
                                });
                            }
                        }
                    }


                    if (rsp.err_code == error.success)
                    {
                        ImageCollection.createPersistedObject(data.ToBsonDocument(), (result) =>
                        {
                            if (result != hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                            {
                                log.log.err("save image falid {0}", data.ToJson());
                            }
                        });

                        rsp.img_essay.creator = user_account;
                        rsp.img_essay.creator_name = name;
                        rsp.img_essay.create_time = data.create_time;
                        rsp.img_essay.essay_guid = data.essay_guid;
                        rsp.img_essay.essay = new();
                        rsp.img_essay.essay.AddRange(data.free_essay);
                        rsp.img_essay.essay.AddRange(data.pay_essay);
                        rsp.img_essay.prompt = data.prompt;
                        rsp.img_essay.hotspot = data.hotspot;
                        rsp.img_essay.discuss_count = data.discuss_count;
                    }
                    else
                    {
                        foreach (var order in order_list)
                        {
                            rsp.payment_info_list.Add(new user_payment_info()
                            {
                                order_uuid = order.order_uuid,
                                state = order.state,
                                completeness = (int)(((float)(order.expire_time - timerservice.Tick) / constant.constant.GenImageWaitTime) * 100),
                                url = order.url,
                            });
                        }
                    }

                    await gate._redis_handle.SetData(redis_help.BuildUserOrderCacheKey(user_account), order_list);
                    await gate._redis_handle.UnLock(lock_key, token);

                } while (false);

                using var st_rsp = MemoryStreamPool.mstMgr.GetStream();
                _serializer_user_save_rsp.Pack(st_rsp, user_save_rsp.user_save_rsp_to_protcol(rsp));
                st_rsp.Position = 0;
                await req.Response(StatusCodes.Status200OK, HttpService.buildCrossHeaders(), st_rsp.ToArray());
            }
            catch (System.Exception ex)
            {
                log.log.err("save_img ex:{0}", ex);
            }
        }

        private async Task query(AbelkhanHttpRequest req)
        {
            try
            {
                using var st = MemoryStreamPool.mstMgr.GetStream();
                st.Write(req.Content);
                st.Position = 0;
                var _req = _serializer_user_query_req.Unpack(st);

                var rsp_list = new List<user_payment_info>();

                string user_account = await gate._redis_handle.GetStrData(redis_help.BuildUserTokenAccountKey(_req.token));
                do
                {
                    var lock_key = redis_help.BuildUserOrderCacheLock(user_account);
                    var token = Guid.NewGuid().ToString();
                    await gate._redis_handle.Lock(lock_key, token, 3000);
                    var order_list = await gate._redis_handle.GetData<List<user_order_info>>(redis_help.BuildUserOrderCacheKey(user_account));
                    if (order_list == null)
                    {
                        break;
                    }

                    foreach (var order in order_list)
                    {
                        if (order.expire_time < timerservice.Tick && order.state == gen_image_state.in_queue)
                        {
                            order.state = gen_image_state.in_generating;
                            order.expire_time = timerservice.Tick + constant.constant.GenImageWaitTime;

                            var order_gen = new order()
                            {
                                order_uuid = order.order_uuid,
                                account = user_account,
                                prompt = order.prompt
                            };
                            await gate._redis_handle.LPushData(constant.constant.GenImageOrderQueue, order_gen);
                        }

                        var _rsp = new user_payment_info()
                        {
                            order_uuid = order.order_uuid,
                            state = order.state,
                            completeness = (int)(((float)(order.expire_time - timerservice.Tick) / constant.constant.GenImageWaitTime) * 100),
                            url = order.url
                        };
                        rsp_list.Add(_rsp);
                    }
                    await gate._redis_handle.SetData(redis_help.BuildUserOrderCacheKey(user_account), order_list);
                    await gate._redis_handle.UnLock(lock_key, token);

                } while (false);

                var rsp = new user_payment_rsp()
                {
                    code = error.success,
                    payment_info_list = rsp_list
                };
                using var st_rsp = MemoryStreamPool.mstMgr.GetStream();
                _serializer_user_payment_rsp.Pack(st_rsp, user_payment_rsp.user_payment_rsp_to_protcol(rsp));
                st_rsp.Position = 0;
                await req.Response(StatusCodes.Status200OK, HttpService.buildCrossHeaders(), st_rsp.ToArray());
            }
            catch (System.Exception ex)
            {
                log.log.err("guest_login ex：{0}", ex);
            }
        }

        private async Task payment_quicken(AbelkhanHttpRequest req)
        {
            try
            {
                using var st = MemoryStreamPool.mstMgr.GetStream();
                st.Write(req.Content);
                st.Position = 0;
                var _req = _serializer_user_payment_req.Unpack(st);

                var rsp = new user_payment_rsp();
                var rsp_list = new List<user_payment_info>();

                string user_account = await gate._redis_handle.GetStrData(redis_help.BuildUserTokenAccountKey(_req.token));

                var lock_key = redis_help.BuildUserOrderCacheLock(user_account);
                var token = Guid.NewGuid().ToString();
                await gate._redis_handle.Lock(lock_key, token, 3000);
                var order_list = await gate._redis_handle.GetData<List<user_order_info>>(redis_help.BuildUserOrderCacheKey(user_account));
                if (order_list == null)
                {
                    order_list = new();
                }
                foreach(var order in order_list)
                {
                    if (order.order_uuid == _req.order_uuid)
                    {
                        int num = 0;
                        if (_req.type == coin_type.money)
                        {
                            // to_do
                        }

                        string data_name = await gate._redis_handle.GetStrData(redis_help.BuildUserDataCacheKey(user_account));
                        if (!gate._data_mgr.get_data_proxy(data_name, out data_proxy _proxy))
                        {
                            _proxy = gate._data_mgr.random_data_proxy();
                        }
                        rsp.code = await _proxy.payment_verify(_req.token, _req.type, num);
                        if (rsp.code == error.success)
                        {
                            order.state = gen_image_state.in_generating;
                            order.expire_time = timerservice.Tick + constant.constant.GenImageWaitTime;

                            var order_gen = new order()
                            {
                                order_uuid = order.order_uuid,
                                account = user_account,
                                prompt = order.prompt
                            };
                            await gate._redis_handle.LPushData(constant.constant.GenImageOrderQueue, order_gen);
                        }
                    }

                    var _rsp = new user_payment_info()
                    {
                        order_uuid = order.order_uuid,
                        state = order.state,
                        completeness = (int)(((float)(order.expire_time - timerservice.Tick) / constant.constant.GenImageWaitTime) * 100),
                        url = order.url
                    };
                    rsp_list.Add(_rsp);
                }
                await gate._redis_handle.SetData(redis_help.BuildUserOrderCacheKey(user_account), order_list);
                await gate._redis_handle.UnLock(lock_key, token);

                rsp.payment_info_list = rsp_list;
                using var st_rsp = MemoryStreamPool.mstMgr.GetStream();
                _serializer_user_payment_rsp.Pack(st_rsp, user_payment_rsp.user_payment_rsp_to_protcol(rsp));
                st_rsp.Position = 0;
                await req.Response(StatusCodes.Status200OK, HttpService.buildCrossHeaders(), st_rsp.ToArray());
            }
            catch (System.Exception ex)
            {
                log.log.err("guest_login ex：{0}", ex);
            }
        }

        private async Task payment_order(AbelkhanHttpRequest req)
        {
            try
            {
                using var st = MemoryStreamPool.mstMgr.GetStream();
                st.Write(req.Content);
                st.Position = 0;
                var _req = _serializer_payment_order_req.Unpack(st);

                var rsp = new user_payment_rsp();
                var rsp_list = new List<user_payment_info>();

                string user_account = await gate._redis_handle.GetStrData(redis_help.BuildUserTokenAccountKey(_req.token));
                int num = 0;
                if (_req.type == coin_type.money)
                {
                    // to_do
                }

                var lock_key = redis_help.BuildUserOrderCacheLock(user_account);
                var token = Guid.NewGuid().ToString();
                await gate._redis_handle.Lock(lock_key, token, 3000);
                var order_list = await gate._redis_handle.GetData<List<user_order_info>>(redis_help.BuildUserOrderCacheKey(user_account));
                if (order_list == null)
                {
                    order_list = new();
                }

                string data_name = await gate._redis_handle.GetStrData(redis_help.BuildUserDataCacheKey(user_account));
                if (!gate._data_mgr.get_data_proxy(data_name, out data_proxy _proxy))
                {
                    _proxy = gate._data_mgr.random_data_proxy();
                }
                rsp.code = await _proxy.payment_verify(_req.token, _req.type, num);
                if (rsp.code == error.success)
                {
                    var new_order = new user_order_info()
                    {
                        order_uuid = Guid.NewGuid().ToString("N"),
                        user_name = _req.name,
                        state = gen_image_state.in_generating,
                        expire_time = timerservice.Tick + constant.constant.GenImageWaitTime,
                        prompt = _req.prompt
                    };
                    order_list.Add(new_order);
                    await gate._redis_handle.SetData(redis_help.BuildUserOrderCacheKey(user_account), order_list);

                    var order_gen = new order()
                    {
                        order_uuid = new_order.order_uuid,
                        account = user_account,
                        prompt = new_order.prompt
                    };
                    await gate._redis_handle.LPushData(constant.constant.GenImageOrderQueue, order_gen);
                }
                await gate._redis_handle.UnLock(lock_key, token);

                foreach (var order in order_list)
                {
                    var _rsp = new user_payment_info()
                    {
                        order_uuid = order.order_uuid,
                        state = order.state,
                        completeness = (int)(((float)(order.expire_time - timerservice.Tick) / constant.constant.GenImageWaitTime) * 100),
                        url = order.url
                    };
                    rsp_list.Add(_rsp);
                }

                rsp.payment_info_list = rsp_list;
                using var st_rsp = MemoryStreamPool.mstMgr.GetStream();
                _serializer_user_payment_rsp.Pack(st_rsp, user_payment_rsp.user_payment_rsp_to_protcol(rsp));
                st_rsp.Position = 0;
                await req.Response(StatusCodes.Status200OK, HttpService.buildCrossHeaders(), st_rsp.ToArray());
            }
            catch (System.Exception ex)
            {
                log.log.err("guest_login ex：{0}", ex);
            }
        }

        private async Task free_order(AbelkhanHttpRequest req)
        {
            try
            {
                using var st = MemoryStreamPool.mstMgr.GetStream();
                st.Write(req.Content);
                st.Position = 0;
                var _req = _serializer_free_order_req.Unpack(st);

                var rsp_list = new List<user_payment_info>();

                string user_account = await gate._redis_handle.GetStrData(redis_help.BuildUserTokenAccountKey(_req.token));

                var lock_key = redis_help.BuildUserOrderCacheLock(user_account);
                var token = Guid.NewGuid().ToString();
                await gate._redis_handle.Lock(lock_key, token, 3000);
                var order_list = await gate._redis_handle.GetData<List<user_order_info> >(redis_help.BuildUserOrderCacheKey(user_account));
                if (order_list == null)
                {
                    order_list = new();
                }
                var new_order = new user_order_info()
                {
                    order_uuid = Guid.NewGuid().ToString("N"),
                    user_name = _req.name,
                    state = gen_image_state.in_queue,
                    expire_time = timerservice.Tick + constant.constant.FreeOrderWaitTime,
                    prompt = _req.prompt
                };
                order_list.Add(new_order);
                await gate._redis_handle.SetData(redis_help.BuildUserOrderCacheKey(user_account), order_list);
                await gate._redis_handle.UnLock(lock_key, token);

                foreach (var order in order_list)
                {
                    var _rsp = new user_payment_info()
                    {
                        order_uuid = order.order_uuid,
                        state = order.state,
                        completeness = (int)(((float)(order.expire_time - timerservice.Tick) / constant.constant.FreeOrderWaitTime) * 100),
                        url = order.url
                    };
                    rsp_list.Add(_rsp);
                }
                var rsp = new user_payment_rsp()
                {
                    code = error.success,
                    payment_info_list = rsp_list
                };
                using var st_rsp = MemoryStreamPool.mstMgr.GetStream();
                _serializer_user_payment_rsp.Pack(st_rsp, user_payment_rsp.user_payment_rsp_to_protcol(rsp));
                st_rsp.Position = 0;
                await req.Response(StatusCodes.Status200OK, HttpService.buildCrossHeaders(), st_rsp.ToArray());
            }
            catch (System.Exception ex)
            {
                log.log.err("guest_login ex：{0}", ex);
            }
        }
    }
}
