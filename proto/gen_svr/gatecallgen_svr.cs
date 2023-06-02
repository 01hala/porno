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
    public class gate_gen_save_img_cb
    {
        private UInt64 cb_uuid;
        private gate_gen_rsp_cb module_rsp_cb;

        public gate_gen_save_img_cb(UInt64 _cb_uuid, gate_gen_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action<string> on_save_img_cb;
        public event Action<error> on_save_img_err;
        public event Action on_save_img_timeout;

        public gate_gen_save_img_cb callBack(Action<string> cb, Action<error> err)
        {
            on_save_img_cb += cb;
            on_save_img_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.save_img_timeout(cb_uuid);
            });
            on_save_img_timeout += timeout_cb;
        }

        public void call_cb(string img_url)
        {
            if (on_save_img_cb != null)
            {
                on_save_img_cb(img_url);
            }
        }

        public void call_err(error err)
        {
            if (on_save_img_err != null)
            {
                on_save_img_err(err);
            }
        }

        public void call_timeout()
        {
            if (on_save_img_timeout != null)
            {
                on_save_img_timeout();
            }
        }

    }

/*this cb code is codegen by abelkhan for c#*/
    public class gate_gen_rsp_cb : common.imodule {
        public Dictionary<UInt64, gate_gen_save_img_cb> map_save_img;
        public gate_gen_rsp_cb()
        {
            map_save_img = new Dictionary<UInt64, gate_gen_save_img_cb>();
            hub.hub._modules.add_mothed("gate_gen_rsp_cb_save_img_rsp", save_img_rsp);
            hub.hub._modules.add_mothed("gate_gen_rsp_cb_save_img_err", save_img_err);
        }

        public void save_img_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _img_url = ((MsgPack.MessagePackObject)inArray[1]).AsString();
            var rsp = try_get_and_del_save_img_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb(_img_url);
            }
        }

        public void save_img_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _err = (error)((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_save_img_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_err);
            }
        }

        public void save_img_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_save_img_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private gate_gen_save_img_cb try_get_and_del_save_img_cb(UInt64 uuid){
            lock(map_save_img)
            {
                if (map_save_img.TryGetValue(uuid, out gate_gen_save_img_cb rsp))
                {
                    map_save_img.Remove(uuid);
                }
                return rsp;
            }
        }

    }

    public class gate_gen_caller {
        public static gate_gen_rsp_cb rsp_cb_gate_gen_handle = null;
        private ThreadLocal<gate_gen_hubproxy> _hubproxy;
        public gate_gen_caller()
        {
            if (rsp_cb_gate_gen_handle == null)
            {
                rsp_cb_gate_gen_handle = new gate_gen_rsp_cb();
            }
            _hubproxy = new ThreadLocal<gate_gen_hubproxy>();
        }

        public gate_gen_hubproxy get_hub(string hub_name) {
            if (_hubproxy.Value == null)
{
                _hubproxy.Value = new gate_gen_hubproxy(rsp_cb_gate_gen_handle);
            }
            _hubproxy.Value.hub_name_0ee4ef2f_36d6_30e9_b44a_0d3340061a3c = hub_name;
            return _hubproxy.Value;
        }

    }

    public class gate_gen_hubproxy {
        public string hub_name_0ee4ef2f_36d6_30e9_b44a_0d3340061a3c;
        private Int32 uuid_0ee4ef2f_36d6_30e9_b44a_0d3340061a3c = (Int32)RandomUUID.random();

        private gate_gen_rsp_cb rsp_cb_gate_gen_handle;

        public gate_gen_hubproxy(gate_gen_rsp_cb rsp_cb_gate_gen_handle_)
        {
            rsp_cb_gate_gen_handle = rsp_cb_gate_gen_handle_;
        }

        public gate_gen_save_img_cb save_img(string account, string order_uuid, save_image_state save_state){
            var uuid_af2541c9_ad7e_567c_bb8b_1a54c9ff8c76 = (UInt64)Interlocked.Increment(ref uuid_0ee4ef2f_36d6_30e9_b44a_0d3340061a3c);

            var _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b = new ArrayList();
            _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b.Add(uuid_af2541c9_ad7e_567c_bb8b_1a54c9ff8c76);
            _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b.Add(account);
            _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b.Add(order_uuid);
            _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b.Add((int)save_state);
            hub.hub._hubs.call_hub(hub_name_0ee4ef2f_36d6_30e9_b44a_0d3340061a3c, "gate_gen_save_img", _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b);

            var cb_save_img_obj = new gate_gen_save_img_cb(uuid_af2541c9_ad7e_567c_bb8b_1a54c9ff8c76, rsp_cb_gate_gen_handle);
            lock(rsp_cb_gate_gen_handle.map_save_img)
            {
                rsp_cb_gate_gen_handle.map_save_img.Add(uuid_af2541c9_ad7e_567c_bb8b_1a54c9ff8c76, cb_save_img_obj);
            }
            return cb_save_img_obj;
        }

    }
/*this module code is codegen by abelkhan codegen for c#*/
    public class gate_gen_save_img_rsp : common.Response {
        private string _hub_name_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b;
        private UInt64 uuid_e3af75d1_4925_3871_a262_bcdded06d2ca;
        public gate_gen_save_img_rsp(string hub_name, UInt64 _uuid) 
        {
            _hub_name_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b = hub_name;
            uuid_e3af75d1_4925_3871_a262_bcdded06d2ca = _uuid;
        }

        public void rsp(string img_url_3d9f3506_268b_35f7_be41_af6c54dff224){
            var _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b = new ArrayList();
            _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b.Add(uuid_e3af75d1_4925_3871_a262_bcdded06d2ca);
            _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b.Add(img_url_3d9f3506_268b_35f7_be41_af6c54dff224);
            hub.hub._hubs.call_hub(_hub_name_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b, "gate_gen_rsp_cb_save_img_rsp", _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b);
        }

        public void err(error err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696){
            var _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b = new ArrayList();
            _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b.Add(uuid_e3af75d1_4925_3871_a262_bcdded06d2ca);
            _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b.Add((int)err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696);
            hub.hub._hubs.call_hub(_hub_name_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b, "gate_gen_rsp_cb_save_img_err", _argv_6c07cd0a_bbdb_33d5_a48c_7eeaaed15a2b);
        }

    }

    public class gate_gen_module : common.imodule {
        public gate_gen_module() 
        {
            hub.hub._modules.add_mothed("gate_gen_save_img", save_img);
        }

        public event Action<string, string, save_image_state> on_save_img;
        public void save_img(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _account = ((MsgPack.MessagePackObject)inArray[1]).AsString();
            var _order_uuid = ((MsgPack.MessagePackObject)inArray[2]).AsString();
            var _save_state = (save_image_state)((MsgPack.MessagePackObject)inArray[3]).AsInt32();
            rsp = new gate_gen_save_img_rsp(hub.hub._hubs.current_hubproxy.name, _cb_uuid);
            if (on_save_img != null){
                on_save_img(_account, _order_uuid, _save_state);
            }
            rsp = null;
        }

    }

}
