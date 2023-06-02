using abelkhan;
using hub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace gate
{
    public class personal_proxy
    {
        private readonly gate_personal_caller gate_personal_Caller;
        private readonly hubproxy proxy;

        public personal_proxy(gate_personal_caller _gate_personal_Caller, hubproxy _proxy)
        {
            gate_personal_Caller = _gate_personal_Caller;
            proxy = _proxy;
        }

        public Task<user_query_img_rsp> query_personal(bool is_self, string creator, long current_img_guid)
        {
            var task = new TaskCompletionSource<user_query_img_rsp>();

            gate_personal_Caller.get_hub(proxy.name).query_personal(is_self, creator, current_img_guid).callBack((current_img_guid, img) =>
            {
                task.SetResult(new user_query_img_rsp()
                {
                    code = error.success,
                    current_img_guid = current_img_guid,
                    img_list = img,
                });
            }, (err) =>
            {
                task.SetResult(new user_query_img_rsp()
                {
                    code = err,
                    current_img_guid = 0,
                    img_list = null,
                });
            }).timeout(3000, () =>
            {
                task.SetResult(new user_query_img_rsp()
                {
                    code = error.timeout,
                    current_img_guid = 0,
                    img_list = null,
                });
            });

            return task.Task;
        }
    }

    public class personal_manager
    {
        private readonly gate_personal_caller gate_personal_Caller = new();
        private readonly Dictionary<string, personal_proxy> personals = new();

        public void on_personal(hubproxy _proxy)
        {
            if (_proxy.type == "personal")
            {
                var _personal_proxy = new personal_proxy(gate_personal_Caller, _proxy);
                personals[_proxy.name] = _personal_proxy;
            }
        }

        public personal_proxy random_personal_proxy()
        {
            var personal_list = personals.Values.ToArray();
            if (personal_list.Count() > 0)
            {
                return personal_list[RandomHelper.RandomInt(personal_list.Count())];
            }
            return null;
        }
    }
}
