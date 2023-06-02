using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MsgPack.Serialization;

namespace abelkhan
{
/*this enum code is codegen by abelkhan codegen for c#*/

/*this struct code is codegen by abelkhan codegen for c#*/
/*this caller code is codegen by abelkhan codegen for c#*/
    public class gate_hotspot_query_prompt_cb
    {
        private UInt64 cb_uuid;
        private gate_hotspot_rsp_cb module_rsp_cb;

        public gate_hotspot_query_prompt_cb(UInt64 _cb_uuid, gate_hotspot_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action<UInt32, List<user_img>> on_query_prompt_cb;
        public event Action<error> on_query_prompt_err;
        public event Action on_query_prompt_timeout;

        public gate_hotspot_query_prompt_cb callBack(Action<UInt32, List<user_img>> cb, Action<error> err)
        {
            on_query_prompt_cb += cb;
            on_query_prompt_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.query_prompt_timeout(cb_uuid);
            });
            on_query_prompt_timeout += timeout_cb;
        }

        public void call_cb(UInt32 current_img_page, List<user_img> img_list)
        {
            if (on_query_prompt_cb != null)
            {
                on_query_prompt_cb(current_img_page, img_list);
            }
        }

        public void call_err(error err)
        {
            if (on_query_prompt_err != null)
            {
                on_query_prompt_err(err);
            }
        }

        public void call_timeout()
        {
            if (on_query_prompt_timeout != null)
            {
                on_query_prompt_timeout();
            }
        }

    }

/*this cb code is codegen by abelkhan for c#*/
    public class gate_hotspot_rsp_cb : common.imodule {
        public Dictionary<UInt64, gate_hotspot_query_prompt_cb> map_query_prompt;
        public gate_hotspot_rsp_cb()
        {
            map_query_prompt = new Dictionary<UInt64, gate_hotspot_query_prompt_cb>();
            hub.hub._modules.add_mothed("gate_hotspot_rsp_cb_query_prompt_rsp", query_prompt_rsp);
            hub.hub._modules.add_mothed("gate_hotspot_rsp_cb_query_prompt_err", query_prompt_err);
        }

        public void query_prompt_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _current_img_page = ((MsgPack.MessagePackObject)inArray[1]).AsUInt32();
            var _img_list = new List<user_img>();
            var _protocol_arrayimg_list = ((MsgPack.MessagePackObject)inArray[2]).AsList();
            foreach (var v_f9731e16_2577_5f56_b72c_64427f0b37ee in _protocol_arrayimg_list){
                _img_list.Add(user_img.protcol_to_user_img(((MsgPack.MessagePackObject)v_f9731e16_2577_5f56_b72c_64427f0b37ee).AsDictionary()));
            }
            var rsp = try_get_and_del_query_prompt_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb(_current_img_page, _img_list);
            }
        }

        public void query_prompt_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _err = (error)((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_query_prompt_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_err);
            }
        }

        public void query_prompt_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_query_prompt_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private gate_hotspot_query_prompt_cb try_get_and_del_query_prompt_cb(UInt64 uuid){
            lock(map_query_prompt)
            {
                if (map_query_prompt.TryGetValue(uuid, out gate_hotspot_query_prompt_cb rsp))
                {
                    map_query_prompt.Remove(uuid);
                }
                return rsp;
            }
        }

    }

    public class gate_hotspot_caller {
        public static gate_hotspot_rsp_cb rsp_cb_gate_hotspot_handle = null;
        private ThreadLocal<gate_hotspot_hubproxy> _hubproxy;
        public gate_hotspot_caller()
        {
            if (rsp_cb_gate_hotspot_handle == null)
            {
                rsp_cb_gate_hotspot_handle = new gate_hotspot_rsp_cb();
            }
            _hubproxy = new ThreadLocal<gate_hotspot_hubproxy>();
        }

        public gate_hotspot_hubproxy get_hub(string hub_name) {
            if (_hubproxy.Value == null)
{
                _hubproxy.Value = new gate_hotspot_hubproxy(rsp_cb_gate_hotspot_handle);
            }
            _hubproxy.Value.hub_name_c505263e_9d81_3c52_9d1a_d98c7333c86f = hub_name;
            return _hubproxy.Value;
        }

    }

    public class gate_hotspot_hubproxy {
        public string hub_name_c505263e_9d81_3c52_9d1a_d98c7333c86f;
        private Int32 uuid_c505263e_9d81_3c52_9d1a_d98c7333c86f = (Int32)RandomUUID.random();

        private gate_hotspot_rsp_cb rsp_cb_gate_hotspot_handle;

        public gate_hotspot_hubproxy(gate_hotspot_rsp_cb rsp_cb_gate_hotspot_handle_)
        {
            rsp_cb_gate_hotspot_handle = rsp_cb_gate_hotspot_handle_;
        }

        public gate_hotspot_query_prompt_cb query_prompt(List<string> prompt, UInt32 current_img_page){
            var uuid_c40d879d_645f_5951_a299_9c20ea4d03fe = (UInt64)Interlocked.Increment(ref uuid_c505263e_9d81_3c52_9d1a_d98c7333c86f);

            var _argv_95d14a2c_5aad_3777_83da_cdf04dcac564 = new ArrayList();
            _argv_95d14a2c_5aad_3777_83da_cdf04dcac564.Add(uuid_c40d879d_645f_5951_a299_9c20ea4d03fe);
            var _array_c5d49c31_8b28_3510_8db8_62415f6795b0 = new ArrayList();
            foreach(var v_8c317482_698c_54c0_8518_b1f383176108 in prompt){
                _array_c5d49c31_8b28_3510_8db8_62415f6795b0.Add(v_8c317482_698c_54c0_8518_b1f383176108);
            }
            _argv_95d14a2c_5aad_3777_83da_cdf04dcac564.Add(_array_c5d49c31_8b28_3510_8db8_62415f6795b0);
            _argv_95d14a2c_5aad_3777_83da_cdf04dcac564.Add(current_img_page);
            hub.hub._hubs.call_hub(hub_name_c505263e_9d81_3c52_9d1a_d98c7333c86f, "gate_hotspot_query_prompt", _argv_95d14a2c_5aad_3777_83da_cdf04dcac564);

            var cb_query_prompt_obj = new gate_hotspot_query_prompt_cb(uuid_c40d879d_645f_5951_a299_9c20ea4d03fe, rsp_cb_gate_hotspot_handle);
            lock(rsp_cb_gate_hotspot_handle.map_query_prompt)
            {
                rsp_cb_gate_hotspot_handle.map_query_prompt.Add(uuid_c40d879d_645f_5951_a299_9c20ea4d03fe, cb_query_prompt_obj);
            }
            return cb_query_prompt_obj;
        }

    }
/*this module code is codegen by abelkhan codegen for c#*/
    public class gate_hotspot_query_prompt_rsp : common.Response {
        private string _hub_name_95d14a2c_5aad_3777_83da_cdf04dcac564;
        private UInt64 uuid_34d50dbb_7319_3e86_8d17_21b3996ed8b5;
        public gate_hotspot_query_prompt_rsp(string hub_name, UInt64 _uuid) 
        {
            _hub_name_95d14a2c_5aad_3777_83da_cdf04dcac564 = hub_name;
            uuid_34d50dbb_7319_3e86_8d17_21b3996ed8b5 = _uuid;
        }

        public void rsp(UInt32 current_img_page_94601d5a_77b3_3047_9025_a4ddf7bb182e, List<user_img> img_list_56b1d035_c100_37c5_b201_96394b0d0c58){
            var _argv_95d14a2c_5aad_3777_83da_cdf04dcac564 = new ArrayList();
            _argv_95d14a2c_5aad_3777_83da_cdf04dcac564.Add(uuid_34d50dbb_7319_3e86_8d17_21b3996ed8b5);
            _argv_95d14a2c_5aad_3777_83da_cdf04dcac564.Add(current_img_page_94601d5a_77b3_3047_9025_a4ddf7bb182e);
            var _array_56b1d035_c100_37c5_b201_96394b0d0c58 = new ArrayList();
            foreach(var v_f9731e16_2577_5f56_b72c_64427f0b37ee in img_list_56b1d035_c100_37c5_b201_96394b0d0c58){
                _array_56b1d035_c100_37c5_b201_96394b0d0c58.Add(user_img.user_img_to_protcol(v_f9731e16_2577_5f56_b72c_64427f0b37ee));
            }
            _argv_95d14a2c_5aad_3777_83da_cdf04dcac564.Add(_array_56b1d035_c100_37c5_b201_96394b0d0c58);
            hub.hub._hubs.call_hub(_hub_name_95d14a2c_5aad_3777_83da_cdf04dcac564, "gate_hotspot_rsp_cb_query_prompt_rsp", _argv_95d14a2c_5aad_3777_83da_cdf04dcac564);
        }

        public void err(error err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696){
            var _argv_95d14a2c_5aad_3777_83da_cdf04dcac564 = new ArrayList();
            _argv_95d14a2c_5aad_3777_83da_cdf04dcac564.Add(uuid_34d50dbb_7319_3e86_8d17_21b3996ed8b5);
            _argv_95d14a2c_5aad_3777_83da_cdf04dcac564.Add((int)err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696);
            hub.hub._hubs.call_hub(_hub_name_95d14a2c_5aad_3777_83da_cdf04dcac564, "gate_hotspot_rsp_cb_query_prompt_err", _argv_95d14a2c_5aad_3777_83da_cdf04dcac564);
        }

    }

    public class gate_hotspot_module : common.imodule {
        public gate_hotspot_module() 
        {
            hub.hub._modules.add_mothed("gate_hotspot_query_prompt", query_prompt);
        }

        public event Action<List<string>, UInt32> on_query_prompt;
        public void query_prompt(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _prompt = new List<string>();
            var _protocol_arrayprompt = ((MsgPack.MessagePackObject)inArray[1]).AsList();
            foreach (var v_c5d138f8_0e8d_5a14_86bf_dad10a9a3566 in _protocol_arrayprompt){
                _prompt.Add(((MsgPack.MessagePackObject)v_c5d138f8_0e8d_5a14_86bf_dad10a9a3566).AsString());
            }
            var _current_img_page = ((MsgPack.MessagePackObject)inArray[2]).AsUInt32();
            rsp = new gate_hotspot_query_prompt_rsp(hub.hub._hubs.current_hubproxy.name, _cb_uuid);
            if (on_query_prompt != null){
                on_query_prompt(_prompt, _current_img_page);
            }
            rsp = null;
        }

    }

}
