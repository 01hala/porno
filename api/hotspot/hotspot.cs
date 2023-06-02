using abelkhan;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotspot
{
    class hotspot_manager
    {
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

        private List<string> hotspot_prompt;
        private Dictionary<string, List<user_img_db> > hotspot_prompt_img = new();

        public hotspot_manager()
        {
            refresh_hotspot(timerservice.Tick);
        }

        private void refresh_hotspot(long tick)
        {
            var tmp_hotspot_prompt = new List<string>();

            var query = new DBQueryHelper();
            HotspotCollection.getObjectInfoEx(query.query(), 0, constant.constant.HotspotPromptCount, constant.constant.HotspotPromptHotspot, false, (hotspot_array) =>
            {
                foreach (var _hotspot in hotspot_array)
                {
                    var _hotspot_prompt_db = BsonSerializer.Deserialize<hotspot_prompt_db>(_hotspot as BsonDocument);
                    tmp_hotspot_prompt.Add(_hotspot_prompt_db.prompt);
                }
            }, () =>
            {
                hotspot_prompt = tmp_hotspot_prompt;
            });

            hub.hub._timer.addticktime(constant.constant.RefreshHotspotTime, refresh_hotspot);
        }

        private async void refresh_prompt_img(string prompt)
        {
            var tmp_hotspot_img = await get_prompt_img_from_db(prompt);
            hotspot_prompt_img[prompt] = tmp_hotspot_img;

            hub.hub._timer.addticktime(constant.constant.RefreshHotspotTime, (tick) => {
                refresh_prompt_img(prompt);
            });
        }

        private Task<List<user_img_db> > get_prompt_img_from_db(string prompt, int current_img_page = -1, int count = constant.constant.ImageCacheCount * 2)
        {
            var task = new TaskCompletionSource<List<user_img_db> >();

            var query = new DBQueryHelper();
            query.condition(constant.constant.ImageHotspotAccess, (int)save_image_state.save_public);
            query.gte(constant.constant.ImageCreateTime, (long)(timerservice.Tick - constant.constant.ImageHotspotTimeoutTime));
            query.elemListMatchEq(constant.constant.ImageHotspotPrompt, prompt);

            var skip = current_img_page > 0 ? current_img_page : 0;

            var prompt_img = new List<user_img_db>();
            ImageCollection.getObjectInfoEx(query.query(), skip, count, constant.constant.ImageHotspot, false, (hotspot_array) =>
            {
                foreach (var _hotspot in hotspot_array)
                {
                    var _user_img_db = BsonSerializer.Deserialize<user_img_db>(_hotspot as BsonDocument);
                    prompt_img.Add(_user_img_db);
                }
            }, () =>
            {
                task.SetResult(prompt_img);
            });

            return task.Task;
        }

        private Dictionary<long, user_img_db> merge_prompt(Dictionary<long, user_img_db> target, Dictionary<long, user_img_db> mutex,  List<user_img_db> src)
        {
            foreach (var img in src)
            {
                if (mutex == null || !mutex.ContainsKey(img.essay_guid))
                {
                    target[img.essay_guid] = img;
                }
            }
            return target;
        }

        public async Task<List<user_img_db> > get_prompt_img(List<string> prompt, uint current_img_page = 0)
        {
            try
            {
                if (prompt.Count <= 0)
                {
                    prompt.AddRange(hotspot_prompt);
                }

                var hotspot_img_dict = new Dictionary<long, user_img_db>();
                foreach (var p in prompt)
                {
                    if (!hotspot_prompt_img.TryGetValue(p, out List<user_img_db> tmp_hotspot_img))
                    {
                        tmp_hotspot_img = await get_prompt_img_from_db(p);
                        hotspot_prompt_img[p] = tmp_hotspot_img;
                        hotspot_img_dict = merge_prompt(hotspot_img_dict, null, tmp_hotspot_img);

                        hub.hub._timer.addticktime(constant.constant.RefreshHotspotTime, (tick) => {
                            refresh_prompt_img(p);
                        });
                    }
                }

                if (current_img_page < hotspot_img_dict.Count)
                {
                    var hotspot_img = hotspot_img_dict.Values.ToList();
                    var index = (int)(current_img_page >= 0 ? current_img_page : 0);
                    var count = (int)(hotspot_img.Count - current_img_page);
                    count = count > constant.constant.ImagePageCount ? constant.constant.ImagePageCount : count;
                    return hotspot_img.GetRange(index, count);
                }
                else
                {
                    var tmp_hotspot_img_dict = new Dictionary<long, user_img_db>();
                    foreach (var p in prompt)
                    {
                        if (!hotspot_prompt_img.TryGetValue(p, out List<user_img_db> tmp_hotspot_img))
                        {
                            var skip = (int)current_img_page / prompt.Count;
                            tmp_hotspot_img = await get_prompt_img_from_db(p, skip, constant.constant.ImagePageCount);
                            tmp_hotspot_img_dict = merge_prompt(tmp_hotspot_img_dict, hotspot_img_dict, tmp_hotspot_img);
                        }
                    }
                    var hotspot_img = tmp_hotspot_img_dict.Values.ToList();
                    var count = hotspot_img.Count;
                    count = count > constant.constant.ImagePageCount ? constant.constant.ImagePageCount : count;
                    return hotspot_img.GetRange(0, count);
                }
            }
            catch (System.Exception ex)
            {
                log.log.err("get_prompt_img ex:{0}", ex);
            }
            finally
            {
                foreach (var p in prompt)
                {
                    var query = new DBQueryHelper();
                    query.condition("prompt", p);
                    var update = new UpdateDataHelper();
                    update.set("prompt", p);
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

            return null;
        }
    }
}
