using abelkhan;
using service;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace hotspot
{
    class gate_msg_handle
    {
        private readonly gate_hotspot_module _module = new();

        public gate_msg_handle()
        {
            _module.on_query_prompt += _module_on_query_prompt;
        }

        private async void _module_on_query_prompt(List<string> prompt, uint current_img_page)
        {
            log.log.trace("on_query_prompt begin!");

            var rsp = (gate_hotspot_query_prompt_rsp)_module.rsp;
            try
            {
                var tmp_img_list = await hotspot.hotspot_mgr.get_prompt_img(prompt, current_img_page);
                var img_list = new List<user_img>();
                foreach (var img in tmp_img_list)
                {
                    var name = await hotspot._redis_handle.GetStrData(redis_help.BuildUserAccountNameKey(img.account));
                    img_list.Add(new user_img() {
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
                var page_count = (uint)(current_img_page + img_list.Count);
                rsp.rsp(page_count, img_list);
            }
            catch (System.Exception ex)
            {
                log.log.err("on_query_prompt ex:{0}", ex);
                rsp.err(error.system_error);
            }
        }
    }
}
