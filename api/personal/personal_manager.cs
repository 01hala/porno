using abelkhan;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

namespace personal
{
    class personal_manager
    {
        private hub.dbproxyproxy.Collection ImageCollection
        {
            get
            {
                return hub.hub.get_random_dbproxyproxy().getCollection(constant.constant.PornoDBName, constant.constant.ImageDBCollection);
            }
        }

        private Dictionary<string, List<user_img_db> > personal_img_dict = new();

        private Task<List<user_img_db> > get_personal_img_from_db(string creator_account, long current_img_guid = 0, int count = constant.constant.ImageCacheCount)
        {
            var task = new TaskCompletionSource<List<user_img_db> >();

            var query = new DBQueryHelper();
            query.condition(constant.constant.ImageCreator, creator_account);
            query.lte(constant.constant.ImageGuid, current_img_guid);

            var personal_img = new List<user_img_db>();
            ImageCollection.getObjectInfoEx(query.query(), 0, count, constant.constant.ImageGuid, false, (hotspot_array) =>
            {
                foreach (var _hotspot in hotspot_array)
                {
                    var _user_img_db = BsonSerializer.Deserialize<user_img_db>(_hotspot as BsonDocument);
                    personal_img.Add(_user_img_db);
                }
            }, () =>
            {
                task.SetResult(personal_img);
            });

            return task.Task;
        }

        public async Task<List<user_img_db> > get_personal_img(string creator_account, bool is_self = true, long current_img_guid = 0)
        {
            try
            {
                if (!personal_img_dict.TryGetValue(creator_account, out List<user_img_db> personal_img))
                {
                    personal_img = await get_personal_img_from_db(creator_account);
                    personal_img_dict[creator_account] = personal_img;
                }

                if (current_img_guid > personal_img.Last().essay_guid)
                {
                    personal_img = await get_personal_img_from_db(creator_account, current_img_guid);
                    current_img_guid = 0;
                }

                List<user_img_db> tmp_personal_img = null;
                if (is_self)
                {
                    tmp_personal_img = personal_img;
                }
                else
                {
                    tmp_personal_img = new();
                    foreach (var img in personal_img)
                    {
                        if (img.save_state == save_image_state.save_public)
                        {
                            tmp_personal_img.Add(img);
                        }
                    }
                }

                var index = 0;
                for ( ; index < tmp_personal_img.Count; ++index)
                {
                    var img = tmp_personal_img[index];
                    if (img.essay_guid < current_img_guid)
                    {
                        break;
                    }
                }

                var count = tmp_personal_img.Count - index;
                count = count > constant.constant.ImagePageCount ? constant.constant.ImagePageCount : count;
                return tmp_personal_img.GetRange(index, count);
            }
            catch (System.Exception ex)
            {
                log.log.err("get_personal_img ex:{0}", ex);
            }

            return new List<user_img_db>();
        }
    }
}
