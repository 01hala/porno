using abelkhan;
using service;
using System;
using System.Collections.Generic;

namespace personal
{
    class gate_msg_handle
    {
        private gate_personal_module _module = new();

        public gate_msg_handle()
        {
            _module.on_query_personal += _module_on_query_personal;
        }

        private async void _module_on_query_personal(bool is_self, string creator, long current_img_guid)
        {
            log.log.trace("on_query_personal begin!");

            var rsp = (gate_personal_query_personal_rsp)_module.rsp;
            try
            {
                if (current_img_guid <= 0)
                {
                    current_img_guid = long.MaxValue;
                }
                var tmp_img_list = await personal.personal_mgr.get_personal_img(creator, is_self, current_img_guid);
                var img_list = new List<user_img>();
                foreach (var img in tmp_img_list)
                {
                    if (img.essay_guid < current_img_guid)
                    {
                        current_img_guid = img.essay_guid;
                    }

                    var name = await personal._redis_handle.GetStrData(redis_help.BuildUserAccountNameKey(img.account));
                    img_list.Add(new user_img()
                    {
                        creator = img.account,
                        creator_name = name,
                        create_time = img.create_time,
                        essay_guid = img.essay_guid,
                        essay = img.free_essay,
                        prompt = img.prompt,
                        hotspot = img.hotspot,
                        discuss_count = img.discuss_count,
                    });
                }
                rsp.rsp(current_img_guid, img_list);
            }
            catch (System.Exception ex)
            {
                log.log.err("on_query_personal ex:{0}", ex);
                rsp.err(error.system_error);
            }
        }
    }
}
