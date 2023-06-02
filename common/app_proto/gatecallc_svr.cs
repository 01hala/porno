using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MsgPack.Serialization;

namespace abelkhan
{
/*this enum code is codegen by abelkhan codegen for c#*/

/*this struct code is codegen by abelkhan codegen for c#*/
    public class user_data_rsp
    {
        public error code;
        public user_data data;
        public string token;
        public static MsgPack.MessagePackObjectDictionary user_data_rsp_to_protcol(user_data_rsp _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("code", (Int32)_struct.code);
            _protocol.Add("data", new MsgPack.MessagePackObject(user_data.user_data_to_protcol(_struct.data)));
            _protocol.Add("token", _struct.token);
            return _protocol;
        }
        public static user_data_rsp protcol_to_user_data_rsp(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _structf2c97d3a_a8fb_31b9_aaaf_89b29c3fa744 = new user_data_rsp();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "code"){
                    _structf2c97d3a_a8fb_31b9_aaaf_89b29c3fa744.code = (error)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "data"){
                    _structf2c97d3a_a8fb_31b9_aaaf_89b29c3fa744.data = user_data.protcol_to_user_data(((MsgPack.MessagePackObject)i.Value).AsDictionary());
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "token"){
                    _structf2c97d3a_a8fb_31b9_aaaf_89b29c3fa744.token = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
            }
            return _structf2c97d3a_a8fb_31b9_aaaf_89b29c3fa744;
        }
    }

    public class user_payment_rsp
    {
        public error code;
        public List<user_payment_info> payment_info_list;
        public static MsgPack.MessagePackObjectDictionary user_payment_rsp_to_protcol(user_payment_rsp _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("code", (Int32)_struct.code);
            if (_struct.payment_info_list != null) {
                var _array_payment_info_list = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.payment_info_list){
                    _array_payment_info_list.Add( new MsgPack.MessagePackObject(user_payment_info.user_payment_info_to_protcol(v_)));
                }
                _protocol.Add("payment_info_list", new MsgPack.MessagePackObject(_array_payment_info_list));
            }
            return _protocol;
        }
        public static user_payment_rsp protcol_to_user_payment_rsp(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _structbfac2878_b521_3950_89f7_63b222746bfe = new user_payment_rsp();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "code"){
                    _structbfac2878_b521_3950_89f7_63b222746bfe.code = (error)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "payment_info_list"){
                    _structbfac2878_b521_3950_89f7_63b222746bfe.payment_info_list = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _structbfac2878_b521_3950_89f7_63b222746bfe.payment_info_list.Add(user_payment_info.protcol_to_user_payment_info(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
            }
            return _structbfac2878_b521_3950_89f7_63b222746bfe;
        }
    }

    public class user_query_img_rsp
    {
        public error code;
        public UInt32 current_img_page;
        public Int64 current_img_guid;
        public List<user_img> img_list;
        public static MsgPack.MessagePackObjectDictionary user_query_img_rsp_to_protcol(user_query_img_rsp _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("code", (Int32)_struct.code);
            _protocol.Add("current_img_page", _struct.current_img_page);
            _protocol.Add("current_img_guid", _struct.current_img_guid);
            if (_struct.img_list != null) {
                var _array_img_list = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.img_list){
                    _array_img_list.Add( new MsgPack.MessagePackObject(user_img.user_img_to_protcol(v_)));
                }
                _protocol.Add("img_list", new MsgPack.MessagePackObject(_array_img_list));
            }
            return _protocol;
        }
        public static user_query_img_rsp protcol_to_user_query_img_rsp(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct8179c93b_f0db_3653_a7a5_ac7baf19da89 = new user_query_img_rsp();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "code"){
                    _struct8179c93b_f0db_3653_a7a5_ac7baf19da89.code = (error)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "current_img_page"){
                    _struct8179c93b_f0db_3653_a7a5_ac7baf19da89.current_img_page = ((MsgPack.MessagePackObject)i.Value).AsUInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "current_img_guid"){
                    _struct8179c93b_f0db_3653_a7a5_ac7baf19da89.current_img_guid = ((MsgPack.MessagePackObject)i.Value).AsInt64();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "img_list"){
                    _struct8179c93b_f0db_3653_a7a5_ac7baf19da89.img_list = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct8179c93b_f0db_3653_a7a5_ac7baf19da89.img_list.Add(user_img.protcol_to_user_img(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
            }
            return _struct8179c93b_f0db_3653_a7a5_ac7baf19da89;
        }
    }

    public class user_save_rsp
    {
        public error err_code;
        public List<user_payment_info> payment_info_list;
        public user_img img_essay;
        public static MsgPack.MessagePackObjectDictionary user_save_rsp_to_protcol(user_save_rsp _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("err_code", (Int32)_struct.err_code);
            if (_struct.payment_info_list != null) {
                var _array_payment_info_list = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.payment_info_list){
                    _array_payment_info_list.Add( new MsgPack.MessagePackObject(user_payment_info.user_payment_info_to_protcol(v_)));
                }
                _protocol.Add("payment_info_list", new MsgPack.MessagePackObject(_array_payment_info_list));
            }
            _protocol.Add("img_essay", new MsgPack.MessagePackObject(user_img.user_img_to_protcol(_struct.img_essay)));
            return _protocol;
        }
        public static user_save_rsp protcol_to_user_save_rsp(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct3d633db2_084a_3840_8ae2_270709aee958 = new user_save_rsp();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "err_code"){
                    _struct3d633db2_084a_3840_8ae2_270709aee958.err_code = (error)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "payment_info_list"){
                    _struct3d633db2_084a_3840_8ae2_270709aee958.payment_info_list = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct3d633db2_084a_3840_8ae2_270709aee958.payment_info_list.Add(user_payment_info.protcol_to_user_payment_info(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "img_essay"){
                    _struct3d633db2_084a_3840_8ae2_270709aee958.img_essay = user_img.protcol_to_user_img(((MsgPack.MessagePackObject)i.Value).AsDictionary());
                }
            }
            return _struct3d633db2_084a_3840_8ae2_270709aee958;
        }
    }

/*this caller code is codegen by abelkhan codegen for c#*/

}
