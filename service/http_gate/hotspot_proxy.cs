using abelkhan;
using hub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace gate
{
    public class hotspot_proxy
    {
        private readonly gate_hotspot_caller gate_hotspot_Caller;
        private readonly hubproxy proxy;

        public hotspot_proxy(gate_hotspot_caller _gate_hotspot_Caller, hubproxy _proxy)
        {
            gate_hotspot_Caller = _gate_hotspot_Caller;
            proxy = _proxy;
        }

        public Task<user_query_img_rsp> query_prompt(List<string> prompt, uint current_img_page)
        {
            var task = new TaskCompletionSource<user_query_img_rsp>();

            gate_hotspot_Caller.get_hub(proxy.name).query_prompt(prompt, current_img_page).callBack((current_img_page, img) =>
            {
                task.SetResult(new user_query_img_rsp()
                {
                    code = error.success,
                    current_img_page = current_img_page,
                    img_list = img,
                });
            }, (err) =>
            {
                task.SetResult(new user_query_img_rsp()
                {
                    code = err,
                    current_img_page = 0,
                    img_list = null,
                });
            }).timeout(3000, () =>
            {
                task.SetResult(new user_query_img_rsp()
                {
                    code = error.timeout,
                    current_img_page = 0,
                    img_list = null,
                });
            });

            return task.Task;
        }
    }

    public class hotspot_manager
    {
        private readonly gate_hotspot_caller gate_hotspot_Caller = new();
        private readonly Dictionary<string, hotspot_proxy> hotsports = new();

        public void on_hotspot(hubproxy _proxy)
        {
            if (_proxy.type == "hotspot")
            {
                var _hotsport_proxy = new hotspot_proxy(gate_hotspot_Caller, _proxy);
                hotsports[_proxy.name] = _hotsport_proxy;
            }
        }

        public hotspot_proxy random_hotsport_proxy()
        {
            var hotsport_list = hotsports.Values.ToArray();
            if (hotsport_list.Count() > 0)
            {
                return hotsport_list[RandomHelper.RandomInt(hotsport_list.Count())];
            }
            return null;
        }
    }
}
