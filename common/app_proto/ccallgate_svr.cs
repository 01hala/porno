using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MsgPack.Serialization;

namespace abelkhan
{
/*this enum code is codegen by abelkhan codegen for c#*/

/*this struct code is codegen by abelkhan codegen for c#*/
    public class guest_req
    {
        public string account;
        public static MsgPack.MessagePackObjectDictionary guest_req_to_protcol(guest_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("account", _struct.account);
            return _protocol;
        }
        public static guest_req protcol_to_guest_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _structa8e23c2d_7d7a_3b5b_aa4c_044edf9aee48 = new guest_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "account"){
                    _structa8e23c2d_7d7a_3b5b_aa4c_044edf9aee48.account = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
            }
            return _structa8e23c2d_7d7a_3b5b_aa4c_044edf9aee48;
        }
    }

    public class user_login_req
    {
        public string account;
        public string password;
        public static MsgPack.MessagePackObjectDictionary user_login_req_to_protcol(user_login_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("account", _struct.account);
            _protocol.Add("password", _struct.password);
            return _protocol;
        }
        public static user_login_req protcol_to_user_login_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct4f4c1b8f_306b_3f0e_bc9c_73c7a66ac3b4 = new user_login_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "account"){
                    _struct4f4c1b8f_306b_3f0e_bc9c_73c7a66ac3b4.account = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "password"){
                    _struct4f4c1b8f_306b_3f0e_bc9c_73c7a66ac3b4.password = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
            }
            return _struct4f4c1b8f_306b_3f0e_bc9c_73c7a66ac3b4;
        }
    }

    public class user_create_req
    {
        public string token;
        public string account;
        public string password;
        public string mail;
        public string name;
        public static MsgPack.MessagePackObjectDictionary user_create_req_to_protcol(user_create_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("token", _struct.token);
            _protocol.Add("account", _struct.account);
            _protocol.Add("password", _struct.password);
            _protocol.Add("mail", _struct.mail);
            _protocol.Add("name", _struct.name);
            return _protocol;
        }
        public static user_create_req protcol_to_user_create_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct5e3b5b8e_c9ea_3a5b_b916_142f40dcfc0f = new user_create_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "token"){
                    _struct5e3b5b8e_c9ea_3a5b_b916_142f40dcfc0f.token = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "account"){
                    _struct5e3b5b8e_c9ea_3a5b_b916_142f40dcfc0f.account = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "password"){
                    _struct5e3b5b8e_c9ea_3a5b_b916_142f40dcfc0f.password = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "mail"){
                    _struct5e3b5b8e_c9ea_3a5b_b916_142f40dcfc0f.mail = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "name"){
                    _struct5e3b5b8e_c9ea_3a5b_b916_142f40dcfc0f.name = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
            }
            return _struct5e3b5b8e_c9ea_3a5b_b916_142f40dcfc0f;
        }
    }

    public class free_order_req
    {
        public string token;
        public string name;
        public List<string> prompt;
        public static MsgPack.MessagePackObjectDictionary free_order_req_to_protcol(free_order_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("token", _struct.token);
            _protocol.Add("name", _struct.name);
            if (_struct.prompt != null) {
                var _array_prompt = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.prompt){
                    _array_prompt.Add(v_);
                }
                _protocol.Add("prompt", new MsgPack.MessagePackObject(_array_prompt));
            }
            return _protocol;
        }
        public static free_order_req protcol_to_free_order_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct389fa851_d8a7_324a_a8e3_4e9a8dc881b1 = new free_order_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "token"){
                    _struct389fa851_d8a7_324a_a8e3_4e9a8dc881b1.token = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "name"){
                    _struct389fa851_d8a7_324a_a8e3_4e9a8dc881b1.name = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "prompt"){
                    _struct389fa851_d8a7_324a_a8e3_4e9a8dc881b1.prompt = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct389fa851_d8a7_324a_a8e3_4e9a8dc881b1.prompt.Add(((MsgPack.MessagePackObject)v_).AsString());
                    }
                }
            }
            return _struct389fa851_d8a7_324a_a8e3_4e9a8dc881b1;
        }
    }

    public class payment_order_req
    {
        public string token;
        public string name;
        public List<string> prompt;
        public coin_type type;
        public static MsgPack.MessagePackObjectDictionary payment_order_req_to_protcol(payment_order_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("token", _struct.token);
            _protocol.Add("name", _struct.name);
            if (_struct.prompt != null) {
                var _array_prompt = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.prompt){
                    _array_prompt.Add(v_);
                }
                _protocol.Add("prompt", new MsgPack.MessagePackObject(_array_prompt));
            }
            _protocol.Add("type", (Int32)_struct.type);
            return _protocol;
        }
        public static payment_order_req protcol_to_payment_order_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct1ca284df_59cc_3bff_a98b_c598787d0e95 = new payment_order_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "token"){
                    _struct1ca284df_59cc_3bff_a98b_c598787d0e95.token = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "name"){
                    _struct1ca284df_59cc_3bff_a98b_c598787d0e95.name = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "prompt"){
                    _struct1ca284df_59cc_3bff_a98b_c598787d0e95.prompt = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct1ca284df_59cc_3bff_a98b_c598787d0e95.prompt.Add(((MsgPack.MessagePackObject)v_).AsString());
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "type"){
                    _struct1ca284df_59cc_3bff_a98b_c598787d0e95.type = (coin_type)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
            }
            return _struct1ca284df_59cc_3bff_a98b_c598787d0e95;
        }
    }

    public class user_payment_req
    {
        public string token;
        public string order_uuid;
        public coin_type type;
        public static MsgPack.MessagePackObjectDictionary user_payment_req_to_protcol(user_payment_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("token", _struct.token);
            _protocol.Add("order_uuid", _struct.order_uuid);
            _protocol.Add("type", (Int32)_struct.type);
            return _protocol;
        }
        public static user_payment_req protcol_to_user_payment_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _structd4dcdc8e_f86e_3dc6_ae3f_ad9606ee886c = new user_payment_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "token"){
                    _structd4dcdc8e_f86e_3dc6_ae3f_ad9606ee886c.token = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "order_uuid"){
                    _structd4dcdc8e_f86e_3dc6_ae3f_ad9606ee886c.order_uuid = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "type"){
                    _structd4dcdc8e_f86e_3dc6_ae3f_ad9606ee886c.type = (coin_type)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
            }
            return _structd4dcdc8e_f86e_3dc6_ae3f_ad9606ee886c;
        }
    }

    public class user_query_req
    {
        public string token;
        public static MsgPack.MessagePackObjectDictionary user_query_req_to_protcol(user_query_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("token", _struct.token);
            return _protocol;
        }
        public static user_query_req protcol_to_user_query_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct354b362a_76ce_351b_9c21_380fd850664b = new user_query_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "token"){
                    _struct354b362a_76ce_351b_9c21_380fd850664b.token = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
            }
            return _struct354b362a_76ce_351b_9c21_380fd850664b;
        }
    }

    public class user_save_req
    {
        public string token;
        public List<order_essay_block> free_essay;
        public List<order_essay_block> pay_essay;
        public save_image_state save_state;
        public static MsgPack.MessagePackObjectDictionary user_save_req_to_protcol(user_save_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("token", _struct.token);
            if (_struct.free_essay != null) {
                var _array_free_essay = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.free_essay){
                    _array_free_essay.Add( new MsgPack.MessagePackObject(order_essay_block.order_essay_block_to_protcol(v_)));
                }
                _protocol.Add("free_essay", new MsgPack.MessagePackObject(_array_free_essay));
            }
            if (_struct.pay_essay != null) {
                var _array_pay_essay = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.pay_essay){
                    _array_pay_essay.Add( new MsgPack.MessagePackObject(order_essay_block.order_essay_block_to_protcol(v_)));
                }
                _protocol.Add("pay_essay", new MsgPack.MessagePackObject(_array_pay_essay));
            }
            _protocol.Add("save_state", (Int32)_struct.save_state);
            return _protocol;
        }
        public static user_save_req protcol_to_user_save_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct7bc7bc9c_b64b_3b13_b301_a88f4356042b = new user_save_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "token"){
                    _struct7bc7bc9c_b64b_3b13_b301_a88f4356042b.token = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "free_essay"){
                    _struct7bc7bc9c_b64b_3b13_b301_a88f4356042b.free_essay = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct7bc7bc9c_b64b_3b13_b301_a88f4356042b.free_essay.Add(order_essay_block.protcol_to_order_essay_block(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "pay_essay"){
                    _struct7bc7bc9c_b64b_3b13_b301_a88f4356042b.pay_essay = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct7bc7bc9c_b64b_3b13_b301_a88f4356042b.pay_essay.Add(order_essay_block.protcol_to_order_essay_block(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "save_state"){
                    _struct7bc7bc9c_b64b_3b13_b301_a88f4356042b.save_state = (save_image_state)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
            }
            return _struct7bc7bc9c_b64b_3b13_b301_a88f4356042b;
        }
    }

    public class user_query_prompt_req
    {
        public List<string> prompt;
        public UInt32 current_img_page;
        public static MsgPack.MessagePackObjectDictionary user_query_prompt_req_to_protcol(user_query_prompt_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            if (_struct.prompt != null) {
                var _array_prompt = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.prompt){
                    _array_prompt.Add(v_);
                }
                _protocol.Add("prompt", new MsgPack.MessagePackObject(_array_prompt));
            }
            _protocol.Add("current_img_page", _struct.current_img_page);
            return _protocol;
        }
        public static user_query_prompt_req protcol_to_user_query_prompt_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _structab90a673_b1fe_3101_9179_488986a39bcb = new user_query_prompt_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "prompt"){
                    _structab90a673_b1fe_3101_9179_488986a39bcb.prompt = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _structab90a673_b1fe_3101_9179_488986a39bcb.prompt.Add(((MsgPack.MessagePackObject)v_).AsString());
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "current_img_page"){
                    _structab90a673_b1fe_3101_9179_488986a39bcb.current_img_page = ((MsgPack.MessagePackObject)i.Value).AsUInt32();
                }
            }
            return _structab90a673_b1fe_3101_9179_488986a39bcb;
        }
    }

    public class user_query_personal_req
    {
        public string token;
        public string personal;
        public Int64 last_img_guid;
        public static MsgPack.MessagePackObjectDictionary user_query_personal_req_to_protcol(user_query_personal_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("token", _struct.token);
            _protocol.Add("personal", _struct.personal);
            _protocol.Add("last_img_guid", _struct.last_img_guid);
            return _protocol;
        }
        public static user_query_personal_req protcol_to_user_query_personal_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _structf63e5eb6_f894_39cd_9a42_f6da0ef72ed4 = new user_query_personal_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "token"){
                    _structf63e5eb6_f894_39cd_9a42_f6da0ef72ed4.token = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "personal"){
                    _structf63e5eb6_f894_39cd_9a42_f6da0ef72ed4.personal = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "last_img_guid"){
                    _structf63e5eb6_f894_39cd_9a42_f6da0ef72ed4.last_img_guid = ((MsgPack.MessagePackObject)i.Value).AsInt64();
                }
            }
            return _structf63e5eb6_f894_39cd_9a42_f6da0ef72ed4;
        }
    }

    public class user_query_home_req
    {
        public string token;
        public Int64 last_img_guid;
        public static MsgPack.MessagePackObjectDictionary user_query_home_req_to_protcol(user_query_home_req _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("token", _struct.token);
            _protocol.Add("last_img_guid", _struct.last_img_guid);
            return _protocol;
        }
        public static user_query_home_req protcol_to_user_query_home_req(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct1abd8ae4_0825_3385_8793_31aa1260fffd = new user_query_home_req();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "token"){
                    _struct1abd8ae4_0825_3385_8793_31aa1260fffd.token = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "last_img_guid"){
                    _struct1abd8ae4_0825_3385_8793_31aa1260fffd.last_img_guid = ((MsgPack.MessagePackObject)i.Value).AsInt64();
                }
            }
            return _struct1abd8ae4_0825_3385_8793_31aa1260fffd;
        }
    }

/*this module code is codegen by abelkhan codegen for c#*/

}
