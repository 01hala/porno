using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MsgPack.Serialization;

namespace abelkhan
{
/*this enum code is codegen by abelkhan codegen for c#*/

/*this struct code is codegen by abelkhan codegen for c#*/
    public class payment
    {
        public string token;
        public coin_type type;
        public Int32 number;
        public static MsgPack.MessagePackObjectDictionary payment_to_protcol(payment _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("token", _struct.token);
            _protocol.Add("type", (Int32)_struct.type);
            _protocol.Add("number", _struct.number);
            return _protocol;
        }
        public static payment protcol_to_payment(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct755a26fb_c2fb_3ce5_a21c_f6138724c31c = new payment();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "token"){
                    _struct755a26fb_c2fb_3ce5_a21c_f6138724c31c.token = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "type"){
                    _struct755a26fb_c2fb_3ce5_a21c_f6138724c31c.type = (coin_type)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "number"){
                    _struct755a26fb_c2fb_3ce5_a21c_f6138724c31c.number = ((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
            }
            return _struct755a26fb_c2fb_3ce5_a21c_f6138724c31c;
        }
    }

/*this caller code is codegen by abelkhan codegen for c#*/
    public class gate_data_guest_login_cb
    {
        private UInt64 cb_uuid;
        private gate_data_rsp_cb module_rsp_cb;

        public gate_data_guest_login_cb(UInt64 _cb_uuid, gate_data_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action<user_data, string> on_guest_login_cb;
        public event Action<error> on_guest_login_err;
        public event Action on_guest_login_timeout;

        public gate_data_guest_login_cb callBack(Action<user_data, string> cb, Action<error> err)
        {
            on_guest_login_cb += cb;
            on_guest_login_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.guest_login_timeout(cb_uuid);
            });
            on_guest_login_timeout += timeout_cb;
        }

        public void call_cb(user_data data, string token)
        {
            if (on_guest_login_cb != null)
            {
                on_guest_login_cb(data, token);
            }
        }

        public void call_err(error err)
        {
            if (on_guest_login_err != null)
            {
                on_guest_login_err(err);
            }
        }

        public void call_timeout()
        {
            if (on_guest_login_timeout != null)
            {
                on_guest_login_timeout();
            }
        }

    }

    public class gate_data_user_login_cb
    {
        private UInt64 cb_uuid;
        private gate_data_rsp_cb module_rsp_cb;

        public gate_data_user_login_cb(UInt64 _cb_uuid, gate_data_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action<user_data, string> on_user_login_cb;
        public event Action<error> on_user_login_err;
        public event Action on_user_login_timeout;

        public gate_data_user_login_cb callBack(Action<user_data, string> cb, Action<error> err)
        {
            on_user_login_cb += cb;
            on_user_login_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.user_login_timeout(cb_uuid);
            });
            on_user_login_timeout += timeout_cb;
        }

        public void call_cb(user_data data, string token)
        {
            if (on_user_login_cb != null)
            {
                on_user_login_cb(data, token);
            }
        }

        public void call_err(error err)
        {
            if (on_user_login_err != null)
            {
                on_user_login_err(err);
            }
        }

        public void call_timeout()
        {
            if (on_user_login_timeout != null)
            {
                on_user_login_timeout();
            }
        }

    }

    public class gate_data_user_create_cb
    {
        private UInt64 cb_uuid;
        private gate_data_rsp_cb module_rsp_cb;

        public gate_data_user_create_cb(UInt64 _cb_uuid, gate_data_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action<user_data, string> on_user_create_cb;
        public event Action<error> on_user_create_err;
        public event Action on_user_create_timeout;

        public gate_data_user_create_cb callBack(Action<user_data, string> cb, Action<error> err)
        {
            on_user_create_cb += cb;
            on_user_create_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.user_create_timeout(cb_uuid);
            });
            on_user_create_timeout += timeout_cb;
        }

        public void call_cb(user_data data, string token)
        {
            if (on_user_create_cb != null)
            {
                on_user_create_cb(data, token);
            }
        }

        public void call_err(error err)
        {
            if (on_user_create_err != null)
            {
                on_user_create_err(err);
            }
        }

        public void call_timeout()
        {
            if (on_user_create_timeout != null)
            {
                on_user_create_timeout();
            }
        }

    }

    public class gate_data_payment_verify_cb
    {
        private UInt64 cb_uuid;
        private gate_data_rsp_cb module_rsp_cb;

        public gate_data_payment_verify_cb(UInt64 _cb_uuid, gate_data_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action on_payment_verify_cb;
        public event Action<error> on_payment_verify_err;
        public event Action on_payment_verify_timeout;

        public gate_data_payment_verify_cb callBack(Action cb, Action<error> err)
        {
            on_payment_verify_cb += cb;
            on_payment_verify_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.payment_verify_timeout(cb_uuid);
            });
            on_payment_verify_timeout += timeout_cb;
        }

        public void call_cb()
        {
            if (on_payment_verify_cb != null)
            {
                on_payment_verify_cb();
            }
        }

        public void call_err(error err)
        {
            if (on_payment_verify_err != null)
            {
                on_payment_verify_err(err);
            }
        }

        public void call_timeout()
        {
            if (on_payment_verify_timeout != null)
            {
                on_payment_verify_timeout();
            }
        }

    }

    public class gate_data_get_user_follow_cb
    {
        private UInt64 cb_uuid;
        private gate_data_rsp_cb module_rsp_cb;

        public gate_data_get_user_follow_cb(UInt64 _cb_uuid, gate_data_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action<List<string>> on_get_user_follow_cb;
        public event Action<error> on_get_user_follow_err;
        public event Action on_get_user_follow_timeout;

        public gate_data_get_user_follow_cb callBack(Action<List<string>> cb, Action<error> err)
        {
            on_get_user_follow_cb += cb;
            on_get_user_follow_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.get_user_follow_timeout(cb_uuid);
            });
            on_get_user_follow_timeout += timeout_cb;
        }

        public void call_cb(List<string> follow_list)
        {
            if (on_get_user_follow_cb != null)
            {
                on_get_user_follow_cb(follow_list);
            }
        }

        public void call_err(error err)
        {
            if (on_get_user_follow_err != null)
            {
                on_get_user_follow_err(err);
            }
        }

        public void call_timeout()
        {
            if (on_get_user_follow_timeout != null)
            {
                on_get_user_follow_timeout();
            }
        }

    }

/*this cb code is codegen by abelkhan for c#*/
    public class gate_data_rsp_cb : common.imodule {
        public Dictionary<UInt64, gate_data_guest_login_cb> map_guest_login;
        public Dictionary<UInt64, gate_data_user_login_cb> map_user_login;
        public Dictionary<UInt64, gate_data_user_create_cb> map_user_create;
        public Dictionary<UInt64, gate_data_payment_verify_cb> map_payment_verify;
        public Dictionary<UInt64, gate_data_get_user_follow_cb> map_get_user_follow;
        public gate_data_rsp_cb()
        {
            map_guest_login = new Dictionary<UInt64, gate_data_guest_login_cb>();
            hub.hub._modules.add_mothed("gate_data_rsp_cb_guest_login_rsp", guest_login_rsp);
            hub.hub._modules.add_mothed("gate_data_rsp_cb_guest_login_err", guest_login_err);
            map_user_login = new Dictionary<UInt64, gate_data_user_login_cb>();
            hub.hub._modules.add_mothed("gate_data_rsp_cb_user_login_rsp", user_login_rsp);
            hub.hub._modules.add_mothed("gate_data_rsp_cb_user_login_err", user_login_err);
            map_user_create = new Dictionary<UInt64, gate_data_user_create_cb>();
            hub.hub._modules.add_mothed("gate_data_rsp_cb_user_create_rsp", user_create_rsp);
            hub.hub._modules.add_mothed("gate_data_rsp_cb_user_create_err", user_create_err);
            map_payment_verify = new Dictionary<UInt64, gate_data_payment_verify_cb>();
            hub.hub._modules.add_mothed("gate_data_rsp_cb_payment_verify_rsp", payment_verify_rsp);
            hub.hub._modules.add_mothed("gate_data_rsp_cb_payment_verify_err", payment_verify_err);
            map_get_user_follow = new Dictionary<UInt64, gate_data_get_user_follow_cb>();
            hub.hub._modules.add_mothed("gate_data_rsp_cb_get_user_follow_rsp", get_user_follow_rsp);
            hub.hub._modules.add_mothed("gate_data_rsp_cb_get_user_follow_err", get_user_follow_err);
        }

        public void guest_login_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _data = user_data.protcol_to_user_data(((MsgPack.MessagePackObject)inArray[1]).AsDictionary());
            var _token = ((MsgPack.MessagePackObject)inArray[2]).AsString();
            var rsp = try_get_and_del_guest_login_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb(_data, _token);
            }
        }

        public void guest_login_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _err = (error)((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_guest_login_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_err);
            }
        }

        public void guest_login_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_guest_login_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private gate_data_guest_login_cb try_get_and_del_guest_login_cb(UInt64 uuid){
            lock(map_guest_login)
            {
                if (map_guest_login.TryGetValue(uuid, out gate_data_guest_login_cb rsp))
                {
                    map_guest_login.Remove(uuid);
                }
                return rsp;
            }
        }

        public void user_login_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _data = user_data.protcol_to_user_data(((MsgPack.MessagePackObject)inArray[1]).AsDictionary());
            var _token = ((MsgPack.MessagePackObject)inArray[2]).AsString();
            var rsp = try_get_and_del_user_login_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb(_data, _token);
            }
        }

        public void user_login_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _err = (error)((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_user_login_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_err);
            }
        }

        public void user_login_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_user_login_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private gate_data_user_login_cb try_get_and_del_user_login_cb(UInt64 uuid){
            lock(map_user_login)
            {
                if (map_user_login.TryGetValue(uuid, out gate_data_user_login_cb rsp))
                {
                    map_user_login.Remove(uuid);
                }
                return rsp;
            }
        }

        public void user_create_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _data = user_data.protcol_to_user_data(((MsgPack.MessagePackObject)inArray[1]).AsDictionary());
            var _token = ((MsgPack.MessagePackObject)inArray[2]).AsString();
            var rsp = try_get_and_del_user_create_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb(_data, _token);
            }
        }

        public void user_create_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _err = (error)((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_user_create_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_err);
            }
        }

        public void user_create_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_user_create_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private gate_data_user_create_cb try_get_and_del_user_create_cb(UInt64 uuid){
            lock(map_user_create)
            {
                if (map_user_create.TryGetValue(uuid, out gate_data_user_create_cb rsp))
                {
                    map_user_create.Remove(uuid);
                }
                return rsp;
            }
        }

        public void payment_verify_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var rsp = try_get_and_del_payment_verify_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb();
            }
        }

        public void payment_verify_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _err = (error)((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_payment_verify_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_err);
            }
        }

        public void payment_verify_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_payment_verify_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private gate_data_payment_verify_cb try_get_and_del_payment_verify_cb(UInt64 uuid){
            lock(map_payment_verify)
            {
                if (map_payment_verify.TryGetValue(uuid, out gate_data_payment_verify_cb rsp))
                {
                    map_payment_verify.Remove(uuid);
                }
                return rsp;
            }
        }

        public void get_user_follow_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _follow_list = new List<string>();
            var _protocol_arrayfollow_list = ((MsgPack.MessagePackObject)inArray[1]).AsList();
            foreach (var v_c8259aa1_0596_522a_b904_594603db44c1 in _protocol_arrayfollow_list){
                _follow_list.Add(((MsgPack.MessagePackObject)v_c8259aa1_0596_522a_b904_594603db44c1).AsString());
            }
            var rsp = try_get_and_del_get_user_follow_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb(_follow_list);
            }
        }

        public void get_user_follow_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _err = (error)((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_get_user_follow_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_err);
            }
        }

        public void get_user_follow_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_get_user_follow_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private gate_data_get_user_follow_cb try_get_and_del_get_user_follow_cb(UInt64 uuid){
            lock(map_get_user_follow)
            {
                if (map_get_user_follow.TryGetValue(uuid, out gate_data_get_user_follow_cb rsp))
                {
                    map_get_user_follow.Remove(uuid);
                }
                return rsp;
            }
        }

    }

    public class gate_data_caller {
        public static gate_data_rsp_cb rsp_cb_gate_data_handle = null;
        private ThreadLocal<gate_data_hubproxy> _hubproxy;
        public gate_data_caller()
        {
            if (rsp_cb_gate_data_handle == null)
            {
                rsp_cb_gate_data_handle = new gate_data_rsp_cb();
            }
            _hubproxy = new ThreadLocal<gate_data_hubproxy>();
        }

        public gate_data_hubproxy get_hub(string hub_name) {
            if (_hubproxy.Value == null)
{
                _hubproxy.Value = new gate_data_hubproxy(rsp_cb_gate_data_handle);
            }
            _hubproxy.Value.hub_name_51d62a86_b290_3597_904c_fa07192dd93d = hub_name;
            return _hubproxy.Value;
        }

    }

    public class gate_data_hubproxy {
        public string hub_name_51d62a86_b290_3597_904c_fa07192dd93d;
        private Int32 uuid_51d62a86_b290_3597_904c_fa07192dd93d = (Int32)RandomUUID.random();

        private gate_data_rsp_cb rsp_cb_gate_data_handle;

        public gate_data_hubproxy(gate_data_rsp_cb rsp_cb_gate_data_handle_)
        {
            rsp_cb_gate_data_handle = rsp_cb_gate_data_handle_;
        }

        public gate_data_guest_login_cb guest_login(string account){
            var uuid_a43baf49_ffed_5c75_803d_b4abf11d3664 = (UInt64)Interlocked.Increment(ref uuid_51d62a86_b290_3597_904c_fa07192dd93d);

            var _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e = new ArrayList();
            _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e.Add(uuid_a43baf49_ffed_5c75_803d_b4abf11d3664);
            _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e.Add(account);
            hub.hub._hubs.call_hub(hub_name_51d62a86_b290_3597_904c_fa07192dd93d, "gate_data_guest_login", _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e);

            var cb_guest_login_obj = new gate_data_guest_login_cb(uuid_a43baf49_ffed_5c75_803d_b4abf11d3664, rsp_cb_gate_data_handle);
            lock(rsp_cb_gate_data_handle.map_guest_login)
            {
                rsp_cb_gate_data_handle.map_guest_login.Add(uuid_a43baf49_ffed_5c75_803d_b4abf11d3664, cb_guest_login_obj);
            }
            return cb_guest_login_obj;
        }

        public gate_data_user_login_cb user_login(string account, string password){
            var uuid_4e753887_b8ad_5d44_a7f8_ea114a5c72ab = (UInt64)Interlocked.Increment(ref uuid_51d62a86_b290_3597_904c_fa07192dd93d);

            var _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331 = new ArrayList();
            _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331.Add(uuid_4e753887_b8ad_5d44_a7f8_ea114a5c72ab);
            _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331.Add(account);
            _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331.Add(password);
            hub.hub._hubs.call_hub(hub_name_51d62a86_b290_3597_904c_fa07192dd93d, "gate_data_user_login", _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331);

            var cb_user_login_obj = new gate_data_user_login_cb(uuid_4e753887_b8ad_5d44_a7f8_ea114a5c72ab, rsp_cb_gate_data_handle);
            lock(rsp_cb_gate_data_handle.map_user_login)
            {
                rsp_cb_gate_data_handle.map_user_login.Add(uuid_4e753887_b8ad_5d44_a7f8_ea114a5c72ab, cb_user_login_obj);
            }
            return cb_user_login_obj;
        }

        public gate_data_user_create_cb user_create(string token, string account, string password, string mail, string name){
            var uuid_8e78dc7a_f690_5c62_8ce3_9a5b1c182c9e = (UInt64)Interlocked.Increment(ref uuid_51d62a86_b290_3597_904c_fa07192dd93d);

            var _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942 = new ArrayList();
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add(uuid_8e78dc7a_f690_5c62_8ce3_9a5b1c182c9e);
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add(token);
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add(account);
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add(password);
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add(mail);
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add(name);
            hub.hub._hubs.call_hub(hub_name_51d62a86_b290_3597_904c_fa07192dd93d, "gate_data_user_create", _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942);

            var cb_user_create_obj = new gate_data_user_create_cb(uuid_8e78dc7a_f690_5c62_8ce3_9a5b1c182c9e, rsp_cb_gate_data_handle);
            lock(rsp_cb_gate_data_handle.map_user_create)
            {
                rsp_cb_gate_data_handle.map_user_create.Add(uuid_8e78dc7a_f690_5c62_8ce3_9a5b1c182c9e, cb_user_create_obj);
            }
            return cb_user_create_obj;
        }

        public gate_data_payment_verify_cb payment_verify(payment _info){
            var uuid_f9c24ee5_84fc_5210_8137_8728b8832f48 = (UInt64)Interlocked.Increment(ref uuid_51d62a86_b290_3597_904c_fa07192dd93d);

            var _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311 = new ArrayList();
            _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311.Add(uuid_f9c24ee5_84fc_5210_8137_8728b8832f48);
            _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311.Add(payment.payment_to_protcol(_info));
            hub.hub._hubs.call_hub(hub_name_51d62a86_b290_3597_904c_fa07192dd93d, "gate_data_payment_verify", _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311);

            var cb_payment_verify_obj = new gate_data_payment_verify_cb(uuid_f9c24ee5_84fc_5210_8137_8728b8832f48, rsp_cb_gate_data_handle);
            lock(rsp_cb_gate_data_handle.map_payment_verify)
            {
                rsp_cb_gate_data_handle.map_payment_verify.Add(uuid_f9c24ee5_84fc_5210_8137_8728b8832f48, cb_payment_verify_obj);
            }
            return cb_payment_verify_obj;
        }

        public gate_data_get_user_follow_cb get_user_follow(string token){
            var uuid_e429ce76_e557_57b3_a6dc_31c6e226ebc9 = (UInt64)Interlocked.Increment(ref uuid_51d62a86_b290_3597_904c_fa07192dd93d);

            var _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14 = new ArrayList();
            _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14.Add(uuid_e429ce76_e557_57b3_a6dc_31c6e226ebc9);
            _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14.Add(token);
            hub.hub._hubs.call_hub(hub_name_51d62a86_b290_3597_904c_fa07192dd93d, "gate_data_get_user_follow", _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14);

            var cb_get_user_follow_obj = new gate_data_get_user_follow_cb(uuid_e429ce76_e557_57b3_a6dc_31c6e226ebc9, rsp_cb_gate_data_handle);
            lock(rsp_cb_gate_data_handle.map_get_user_follow)
            {
                rsp_cb_gate_data_handle.map_get_user_follow.Add(uuid_e429ce76_e557_57b3_a6dc_31c6e226ebc9, cb_get_user_follow_obj);
            }
            return cb_get_user_follow_obj;
        }

    }
/*this module code is codegen by abelkhan codegen for c#*/
    public class gate_data_guest_login_rsp : common.Response {
        private string _hub_name_3ce23e9a_22f4_360c_ab36_fd23208f858e;
        private UInt64 uuid_145cbb6d_e68b_3941_bd5c_aa2c35305b32;
        public gate_data_guest_login_rsp(string hub_name, UInt64 _uuid) 
        {
            _hub_name_3ce23e9a_22f4_360c_ab36_fd23208f858e = hub_name;
            uuid_145cbb6d_e68b_3941_bd5c_aa2c35305b32 = _uuid;
        }

        public void rsp(user_data data_f3243755_077d_3691_ac00_3bfa0fe38efa, string token_6333efe6_4f25_3c9a_a58e_52c6c889a79e){
            var _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e = new ArrayList();
            _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e.Add(uuid_145cbb6d_e68b_3941_bd5c_aa2c35305b32);
            _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e.Add(user_data.user_data_to_protcol(data_f3243755_077d_3691_ac00_3bfa0fe38efa));
            _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e.Add(token_6333efe6_4f25_3c9a_a58e_52c6c889a79e);
            hub.hub._hubs.call_hub(_hub_name_3ce23e9a_22f4_360c_ab36_fd23208f858e, "gate_data_rsp_cb_guest_login_rsp", _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e);
        }

        public void err(error err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696){
            var _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e = new ArrayList();
            _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e.Add(uuid_145cbb6d_e68b_3941_bd5c_aa2c35305b32);
            _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e.Add((int)err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696);
            hub.hub._hubs.call_hub(_hub_name_3ce23e9a_22f4_360c_ab36_fd23208f858e, "gate_data_rsp_cb_guest_login_err", _argv_3ce23e9a_22f4_360c_ab36_fd23208f858e);
        }

    }

    public class gate_data_user_login_rsp : common.Response {
        private string _hub_name_d5863f0a_05c8_39b8_8b07_ae4639c47331;
        private UInt64 uuid_000ed739_577a_3dbe_af40_1363a10035c7;
        public gate_data_user_login_rsp(string hub_name, UInt64 _uuid) 
        {
            _hub_name_d5863f0a_05c8_39b8_8b07_ae4639c47331 = hub_name;
            uuid_000ed739_577a_3dbe_af40_1363a10035c7 = _uuid;
        }

        public void rsp(user_data data_f3243755_077d_3691_ac00_3bfa0fe38efa, string token_6333efe6_4f25_3c9a_a58e_52c6c889a79e){
            var _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331 = new ArrayList();
            _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331.Add(uuid_000ed739_577a_3dbe_af40_1363a10035c7);
            _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331.Add(user_data.user_data_to_protcol(data_f3243755_077d_3691_ac00_3bfa0fe38efa));
            _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331.Add(token_6333efe6_4f25_3c9a_a58e_52c6c889a79e);
            hub.hub._hubs.call_hub(_hub_name_d5863f0a_05c8_39b8_8b07_ae4639c47331, "gate_data_rsp_cb_user_login_rsp", _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331);
        }

        public void err(error err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696){
            var _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331 = new ArrayList();
            _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331.Add(uuid_000ed739_577a_3dbe_af40_1363a10035c7);
            _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331.Add((int)err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696);
            hub.hub._hubs.call_hub(_hub_name_d5863f0a_05c8_39b8_8b07_ae4639c47331, "gate_data_rsp_cb_user_login_err", _argv_d5863f0a_05c8_39b8_8b07_ae4639c47331);
        }

    }

    public class gate_data_user_create_rsp : common.Response {
        private string _hub_name_5641839e_eb9b_37ef_a729_df5ccbf4b942;
        private UInt64 uuid_549a044e_313a_3360_802b_c969920eea26;
        public gate_data_user_create_rsp(string hub_name, UInt64 _uuid) 
        {
            _hub_name_5641839e_eb9b_37ef_a729_df5ccbf4b942 = hub_name;
            uuid_549a044e_313a_3360_802b_c969920eea26 = _uuid;
        }

        public void rsp(user_data data_f3243755_077d_3691_ac00_3bfa0fe38efa, string token_6333efe6_4f25_3c9a_a58e_52c6c889a79e){
            var _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942 = new ArrayList();
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add(uuid_549a044e_313a_3360_802b_c969920eea26);
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add(user_data.user_data_to_protcol(data_f3243755_077d_3691_ac00_3bfa0fe38efa));
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add(token_6333efe6_4f25_3c9a_a58e_52c6c889a79e);
            hub.hub._hubs.call_hub(_hub_name_5641839e_eb9b_37ef_a729_df5ccbf4b942, "gate_data_rsp_cb_user_create_rsp", _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942);
        }

        public void err(error err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696){
            var _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942 = new ArrayList();
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add(uuid_549a044e_313a_3360_802b_c969920eea26);
            _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942.Add((int)err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696);
            hub.hub._hubs.call_hub(_hub_name_5641839e_eb9b_37ef_a729_df5ccbf4b942, "gate_data_rsp_cb_user_create_err", _argv_5641839e_eb9b_37ef_a729_df5ccbf4b942);
        }

    }

    public class gate_data_payment_verify_rsp : common.Response {
        private string _hub_name_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311;
        private UInt64 uuid_5c5bedd0_0c53_362c_8680_239e14ca89b4;
        public gate_data_payment_verify_rsp(string hub_name, UInt64 _uuid) 
        {
            _hub_name_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311 = hub_name;
            uuid_5c5bedd0_0c53_362c_8680_239e14ca89b4 = _uuid;
        }

        public void rsp(){
            var _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311 = new ArrayList();
            _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311.Add(uuid_5c5bedd0_0c53_362c_8680_239e14ca89b4);
            hub.hub._hubs.call_hub(_hub_name_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311, "gate_data_rsp_cb_payment_verify_rsp", _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311);
        }

        public void err(error err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696){
            var _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311 = new ArrayList();
            _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311.Add(uuid_5c5bedd0_0c53_362c_8680_239e14ca89b4);
            _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311.Add((int)err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696);
            hub.hub._hubs.call_hub(_hub_name_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311, "gate_data_rsp_cb_payment_verify_err", _argv_a36552bc_5c9c_3a9d_afdf_f1c4fd93a311);
        }

    }

    public class gate_data_get_user_follow_rsp : common.Response {
        private string _hub_name_bfc31343_09d9_329d_b92b_b7ce9f32dc14;
        private UInt64 uuid_29266070_3650_3cab_b1e0_d9307fc8ff95;
        public gate_data_get_user_follow_rsp(string hub_name, UInt64 _uuid) 
        {
            _hub_name_bfc31343_09d9_329d_b92b_b7ce9f32dc14 = hub_name;
            uuid_29266070_3650_3cab_b1e0_d9307fc8ff95 = _uuid;
        }

        public void rsp(List<string> follow_list_588efc83_9cbc_317a_a3f0_dabadcff9490){
            var _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14 = new ArrayList();
            _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14.Add(uuid_29266070_3650_3cab_b1e0_d9307fc8ff95);
            var _array_588efc83_9cbc_317a_a3f0_dabadcff9490 = new ArrayList();
            foreach(var v_c8259aa1_0596_522a_b904_594603db44c1 in follow_list_588efc83_9cbc_317a_a3f0_dabadcff9490){
                _array_588efc83_9cbc_317a_a3f0_dabadcff9490.Add(v_c8259aa1_0596_522a_b904_594603db44c1);
            }
            _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14.Add(_array_588efc83_9cbc_317a_a3f0_dabadcff9490);
            hub.hub._hubs.call_hub(_hub_name_bfc31343_09d9_329d_b92b_b7ce9f32dc14, "gate_data_rsp_cb_get_user_follow_rsp", _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14);
        }

        public void err(error err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696){
            var _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14 = new ArrayList();
            _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14.Add(uuid_29266070_3650_3cab_b1e0_d9307fc8ff95);
            _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14.Add((int)err_ad2710a2_3dd2_3a8f_a4c8_a7ebbe1df696);
            hub.hub._hubs.call_hub(_hub_name_bfc31343_09d9_329d_b92b_b7ce9f32dc14, "gate_data_rsp_cb_get_user_follow_err", _argv_bfc31343_09d9_329d_b92b_b7ce9f32dc14);
        }

    }

    public class gate_data_module : common.imodule {
        public gate_data_module() 
        {
            hub.hub._modules.add_mothed("gate_data_guest_login", guest_login);
            hub.hub._modules.add_mothed("gate_data_user_login", user_login);
            hub.hub._modules.add_mothed("gate_data_user_create", user_create);
            hub.hub._modules.add_mothed("gate_data_payment_verify", payment_verify);
            hub.hub._modules.add_mothed("gate_data_get_user_follow", get_user_follow);
        }

        public event Action<string> on_guest_login;
        public void guest_login(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _account = ((MsgPack.MessagePackObject)inArray[1]).AsString();
            rsp = new gate_data_guest_login_rsp(hub.hub._hubs.current_hubproxy.name, _cb_uuid);
            if (on_guest_login != null){
                on_guest_login(_account);
            }
            rsp = null;
        }

        public event Action<string, string> on_user_login;
        public void user_login(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _account = ((MsgPack.MessagePackObject)inArray[1]).AsString();
            var _password = ((MsgPack.MessagePackObject)inArray[2]).AsString();
            rsp = new gate_data_user_login_rsp(hub.hub._hubs.current_hubproxy.name, _cb_uuid);
            if (on_user_login != null){
                on_user_login(_account, _password);
            }
            rsp = null;
        }

        public event Action<string, string, string, string, string> on_user_create;
        public void user_create(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _token = ((MsgPack.MessagePackObject)inArray[1]).AsString();
            var _account = ((MsgPack.MessagePackObject)inArray[2]).AsString();
            var _password = ((MsgPack.MessagePackObject)inArray[3]).AsString();
            var _mail = ((MsgPack.MessagePackObject)inArray[4]).AsString();
            var _name = ((MsgPack.MessagePackObject)inArray[5]).AsString();
            rsp = new gate_data_user_create_rsp(hub.hub._hubs.current_hubproxy.name, _cb_uuid);
            if (on_user_create != null){
                on_user_create(_token, _account, _password, _mail, _name);
            }
            rsp = null;
        }

        public event Action<payment> on_payment_verify;
        public void payment_verify(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var __info = payment.protcol_to_payment(((MsgPack.MessagePackObject)inArray[1]).AsDictionary());
            rsp = new gate_data_payment_verify_rsp(hub.hub._hubs.current_hubproxy.name, _cb_uuid);
            if (on_payment_verify != null){
                on_payment_verify(__info);
            }
            rsp = null;
        }

        public event Action<string> on_get_user_follow;
        public void get_user_follow(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _token = ((MsgPack.MessagePackObject)inArray[1]).AsString();
            rsp = new gate_data_get_user_follow_rsp(hub.hub._hubs.current_hubproxy.name, _cb_uuid);
            if (on_get_user_follow != null){
                on_get_user_follow(_token);
            }
            rsp = null;
        }

    }

}
