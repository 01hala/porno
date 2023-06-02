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
    public class gate_personal_query_personal_cb
    {
        private UInt64 cb_uuid;
        private gate_personal_rsp_cb module_rsp_cb;

        public gate_personal_query_personal_cb(UInt64 _cb_uuid, gate_personal_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action<Int64, List<user_img>> on_query_personal_cb;
        public event Action<error> on_query_personal_err;
        public event Action on_query_personal_timeout;

        public gate_personal_query_personal_cb callBack(Action<Int64, List<user_img>> cb, Action<error> err)
        {
            on_query_personal_cb += cb;
            on_query_personal_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.query_personal_timeout(cb_uuid);
            });
            on_query_personal_timeout += timeout_cb;
        }

        public void call_cb(Int64 current_img_guid, List<user_img> img_list)
        {
            if (on_query_personal_cb != null)
            {
                on_query_personal_cb(current_img_guid, img_list);
            }
        }

        public void call_err(error err)
        {
            if (on_query_personal_err != null)
            {
                on_query_personal_err(err);
            }
        }

        public void call_timeout()
        {
            if (on_query_personal_timeout != null)
            {
                on_query_personal_timeout();
            }
        }

    }

/*this cb code is codegen by abelkhan for c#*/
    public class gate_personal_rsp_cb : common.imodule {
        public Dictionary<UInt64, gate_personal_query_personal_cb> map_query_personal;
        public gate_personal_rsp_cb()
        {
            map_query_personal = new Dictionary<UInt64, gate_personal_query_personal_cb>();
            hub.hub._modules.add_mothed("gate_personal_rsp_cb_query_personal_rsp", query_personal_rsp);
            hub.hub._modules.add_mothed("gate_personal_rsp_cb_query_personal_err", query_personal_err);
        }

        public void query_personal_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _current_img_guid = ((MsgPack.MessagePackObject)inArray[1]).AsInt64();
            var _img_list = new List<user_img>();
            var _protocol_arrayimg_list = ((MsgPack.MessagePackObject)inArray[2]).AsList();
            foreach (var v_f9731e16_2577_5f56_b72c_64427f0b37ee in _protocol_arrayimg_list){
                _img_list.Add(user_img.protcol_to_user_img(((MsgPack.MessagePackObject)v_f9731e16_2577_5f56_b72c_64427f0b37ee).AsDictionary()));
            }
            var rsp = try_get_and_del_query_personal_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb(_current_img_guid, _img_list);
            }
        }

        public void query_personal_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _err = (error)((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_query_personal_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_err);
            }
        }

        public void query_personal_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_query_personal_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private gate_personal_query_personal_cb try_get_and_del_query_personal_cb(UInt64 uuid){
            lock(map_query_personal)
            {
                if (map_query_personal.TryGetValue(uuid, out gate_personal_query_personal_cb rsp))
                {
                    map_query_personal.Remove(uuid);
                }
                return rsp;
            }
        }

    }

    public class gate_personal_caller {
        public static gate_personal_rsp_cb rsp_cb_gate_personal_handle = null;
        private ThreadLocal<gate_personal_hubproxy> _hubproxy;
        public gate_personal_caller()
        {
            if (rsp_cb_gate_personal_handle == null)
            {
                rsp_cb_gate_personal_handle = new gate_personal_rsp_cb();
            }
            _hubproxy = new ThreadLocal<gate_personal_hubproxy>();
        }

        public gate_personal_hubproxy get_hub(string hub_name) {
            if (_hubproxy.Value == null)
{
                _hubproxy.Value = new gate_personal_hubproxy(rsp_cb_gate_personal_handle);
            }
            _hubproxy.Value.hub_name_2c6720be_88a9_35a4_82d4_d13cf0ef07f6 = hub_name;
            return _hubproxy.Value;
        }

    }

    public class gate_personal_hubproxy {
        public string hub_name_2c6720be_88a9_35a4_82d4_d13cf0ef07f6;
        private Int32 uuid_2c6720be_88a9_35a4_82d4_d13cf0ef07f6 = (Int32)RandomUUID.random();

        private gate_personal_rsp_cb rsp_cb_gate_personal_handle;

        public gate_personal_hubproxy(gate_personal_rsp_cb rsp_cb_gate_personal_handle_)
        {
            rsp_cb_gate_personal_handle = rsp_cb_gate_personal_handle_;
        }

        public gate_personal_query_personal_cb query_personal(bool is_self, string creator, Int64 last_img_guid){
            var uuid_6a74596c_060b_5e8a_a05c_f39eee381dc3 = (UInt64)Interlocked.Increment(ref uuid_2c6720be_88a9_35a4_82d4_d13cf0ef07f6);

            var _argv_d974faf1_8b47_3e54_954f_982277138e1f = new ArrayList();
            _argv_d974faf1_8b47_3e54_954f_982277138e1f.Add(uuid_6a74596c_060b_5e8a_a05c_f39eee381dc3);
            _argv_d974faf1_8b47_3e54_954f_982277138e1f.Add(is_self);
            _argv_d974faf1_8b47_3e54_954f_982277138e1f.Add(creator);
            _argv_d974faf1_8b47_3e54_954f_982277138e1f.Add(last_img_guid);
            hub.hub._hubs.call_hub(hub_name_2c6720be_88a9_35a4_82d4_d13cf0ef07f6, "gate_personal_query_personal", _argv_d974faf1_8b47_3e54_954f_982277138e1f);

            var cb_query_personal_obj = new gate_personal_query_personal_cb(uuid_6a74596c_060b_5e8a_a05c_f39eee381dc3, rsp_cb_gate_personal_handle);
            lock(rsp_cb_gate_personal_handle.map_query_personal)
            {
                rsp_cb_gate_personal_handle.map_query_personal.Add(uuid_6a74596c_060b_5e8a_a05c_f39eee381dc3, cb_query_personal_obj);
            }
            return cb_query_personal_obj;
        }

    }
/*this module code is codegen by abelkhan codegen for c#*/
    public class gate_personal_query_personal_rsp : common.Response {
        private string _hub_name_d974faf1_8b47_3e54_954f_982277138e1f;
        private UInt64 uuid_191d331c_f9f5_3690_b19a_ef97bd15ea73;
        public gate_personal_query_personal_rsp(string hub_name, UInt64 _uuid) 
        {
            _hub_name_d974faf1_8b47_3e54_954f_982277138e1f = hub_name;
            uuid_191d331c_f9f5_3690_b19a_ef97bd15ea73 = _uuid;
        }

        public void rsp(Int64 current_img_guid_3d3b8cef_5ff4_3f57_88b2_770a4d6e0cfb, List<user_img> img_list_56b1d035_c100_37c5_b201_96394b0d0c58){
            var _argv_d974faf1_8b47_3e54_954f_982277138e1f = new ArrayList();
            _argv_d974faf1_8b47_3e54_954f_982277138e1f.Add(uuid_191d331c_f9f5_3690_b19a_ef97bd15ea73);
            _argv_d974faf1_8b47_3e54_954f_982277138e1f.Add(current_img_guid_3d3b8cef_5ff4_3f57_88b2_770a4d6e0cfb);
            var _array_56b1d035_c100_37c5_b201_96394b0d0c58 = new ArrayList();
            foreach(var v_f9731e16_2577_5f56_b72c_64427f0b37ee in img_list_56b1d035_c100_37c5_b201_96394b0d0c58){
                _array_56b1d035_c100_37c5_b201_96394b0d0c58.Add(user_img.user_img_to_protcol(v_f9731e16_2577_5f56_b72c_64427f0b37ee));
            }
            _argv_d974faf1_8b47_3e54_954f_982277138e1f.Add(_array_56b1d035_c100_37c5_b201_96394b0d0c58);
            hub.hub._hubs.call_hub(_hub_name_d974faf1_8b47_3e54_954f_982277138e1f, "gate_personal_rsp_cb_query_personal_rsp", _argv_d974faf1_8b47_3e54_954f_982277138e1f);
        }

        public void err(error err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696){
            var _argv_d974faf1_8b47_3e54_954f_982277138e1f = new ArrayList();
            _argv_d974faf1_8b47_3e54_954f_982277138e1f.Add(uuid_191d331c_f9f5_3690_b19a_ef97bd15ea73);
            _argv_d974faf1_8b47_3e54_954f_982277138e1f.Add((int)err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696);
            hub.hub._hubs.call_hub(_hub_name_d974faf1_8b47_3e54_954f_982277138e1f, "gate_personal_rsp_cb_query_personal_err", _argv_d974faf1_8b47_3e54_954f_982277138e1f);
        }

    }

    public class gate_personal_module : common.imodule {
        public gate_personal_module() 
        {
            hub.hub._modules.add_mothed("gate_personal_query_personal", query_personal);
        }

        public event Action<bool, string, Int64> on_query_personal;
        public void query_personal(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _is_self = ((MsgPack.MessagePackObject)inArray[1]).AsBoolean();
            var _creator = ((MsgPack.MessagePackObject)inArray[2]).AsString();
            var _last_img_guid = ((MsgPack.MessagePackObject)inArray[3]).AsInt64();
            rsp = new gate_personal_query_personal_rsp(hub.hub._hubs.current_hubproxy.name, _cb_uuid);
            if (on_query_personal != null){
                on_query_personal(_is_self, _creator, _last_img_guid);
            }
            rsp = null;
        }

    }

}
