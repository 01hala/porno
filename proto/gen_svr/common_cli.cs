using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MsgPack.Serialization;

namespace abelkhan
{
/*this enum code is codegen by abelkhan codegen for c#*/

    public enum error{
        system_error = -3,
        timeout = -2,
        db_error = -1,
        success = 0,
        no_exist_user = 1,
        wrong_password = 2,
        no_enough_coin = 3,
        money_number_mismatch = 4,
        img_is_expired = 5,
        account_repeat = 6,
        mail_repeat = 7
    }
    public enum coin_type{
        coin = 1,
        money = 2
    }
    public enum gen_image_state{
        in_queue = 1,
        in_generating = 2,
        done_generate = 3
    }
    public enum save_image_state{
        save_public = 1,
        save_private = 2,
        give_up = 3
    }
/*this struct code is codegen by abelkhan codegen for c#*/
    public class order
    {
        public string order_uuid;
        public string account;
        public List<string> prompt;
        public static MsgPack.MessagePackObjectDictionary order_to_protcol(order _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("order_uuid", _struct.order_uuid);
            _protocol.Add("account", _struct.account);
            if (_struct.prompt != null) {
                var _array_prompt = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.prompt){
                    _array_prompt.Add(v_);
                }
                _protocol.Add("prompt", new MsgPack.MessagePackObject(_array_prompt));
            }
            return _protocol;
        }
        public static order protcol_to_order(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct8d0e4bda_0568_3845_9b82_948160cc0f77 = new order();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "order_uuid"){
                    _struct8d0e4bda_0568_3845_9b82_948160cc0f77.order_uuid = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "account"){
                    _struct8d0e4bda_0568_3845_9b82_948160cc0f77.account = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "prompt"){
                    _struct8d0e4bda_0568_3845_9b82_948160cc0f77.prompt = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct8d0e4bda_0568_3845_9b82_948160cc0f77.prompt.Add(((MsgPack.MessagePackObject)v_).AsString());
                    }
                }
            }
            return _struct8d0e4bda_0568_3845_9b82_948160cc0f77;
        }
    }

    public class user_order_info
    {
        public string order_uuid;
        public string user_name;
        public gen_image_state state;
        public Int64 expire_time;
        public List<string> prompt;
        public string url;
        public Int64 img_guid;
        public string file;
        public string gen_svr;
        public static MsgPack.MessagePackObjectDictionary user_order_info_to_protcol(user_order_info _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("order_uuid", _struct.order_uuid);
            _protocol.Add("user_name", _struct.user_name);
            _protocol.Add("state", (Int32)_struct.state);
            _protocol.Add("expire_time", _struct.expire_time);
            if (_struct.prompt != null) {
                var _array_prompt = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.prompt){
                    _array_prompt.Add(v_);
                }
                _protocol.Add("prompt", new MsgPack.MessagePackObject(_array_prompt));
            }
            _protocol.Add("url", _struct.url);
            _protocol.Add("img_guid", _struct.img_guid);
            _protocol.Add("file", _struct.file);
            _protocol.Add("gen_svr", _struct.gen_svr);
            return _protocol;
        }
        public static user_order_info protcol_to_user_order_info(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f = new user_order_info();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "order_uuid"){
                    _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f.order_uuid = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "user_name"){
                    _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f.user_name = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "state"){
                    _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f.state = (gen_image_state)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "expire_time"){
                    _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f.expire_time = ((MsgPack.MessagePackObject)i.Value).AsInt64();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "prompt"){
                    _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f.prompt = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f.prompt.Add(((MsgPack.MessagePackObject)v_).AsString());
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "url"){
                    _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f.url = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "img_guid"){
                    _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f.img_guid = ((MsgPack.MessagePackObject)i.Value).AsInt64();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "file"){
                    _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f.file = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "gen_svr"){
                    _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f.gen_svr = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
            }
            return _struct4e000f99_853a_3f3f_94fd_fdfed8d38f4f;
        }
    }

    public class user_payment_info
    {
        public string order_uuid;
        public gen_image_state state;
        public Int32 completeness;
        public string url;
        public static MsgPack.MessagePackObjectDictionary user_payment_info_to_protcol(user_payment_info _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("order_uuid", _struct.order_uuid);
            _protocol.Add("state", (Int32)_struct.state);
            _protocol.Add("completeness", _struct.completeness);
            _protocol.Add("url", _struct.url);
            return _protocol;
        }
        public static user_payment_info protcol_to_user_payment_info(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct403a59d9_5759_3870_81f4_c5e0e74f1cc7 = new user_payment_info();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "order_uuid"){
                    _struct403a59d9_5759_3870_81f4_c5e0e74f1cc7.order_uuid = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "state"){
                    _struct403a59d9_5759_3870_81f4_c5e0e74f1cc7.state = (gen_image_state)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "completeness"){
                    _struct403a59d9_5759_3870_81f4_c5e0e74f1cc7.completeness = ((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "url"){
                    _struct403a59d9_5759_3870_81f4_c5e0e74f1cc7.url = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
            }
            return _struct403a59d9_5759_3870_81f4_c5e0e74f1cc7;
        }
    }

    public class user_data
    {
        public string account;
        public string name;
        public Int32 coin;
        public Int32 level;
        public static MsgPack.MessagePackObjectDictionary user_data_to_protcol(user_data _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("account", _struct.account);
            _protocol.Add("name", _struct.name);
            _protocol.Add("coin", _struct.coin);
            _protocol.Add("level", _struct.level);
            return _protocol;
        }
        public static user_data protcol_to_user_data(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct557ac588_ea84_3326_af1a_b327e334fca4 = new user_data();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "account"){
                    _struct557ac588_ea84_3326_af1a_b327e334fca4.account = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "name"){
                    _struct557ac588_ea84_3326_af1a_b327e334fca4.name = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "coin"){
                    _struct557ac588_ea84_3326_af1a_b327e334fca4.coin = ((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "level"){
                    _struct557ac588_ea84_3326_af1a_b327e334fca4.level = ((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
            }
            return _struct557ac588_ea84_3326_af1a_b327e334fca4;
        }
    }

    public class order_essay_block
    {
        public string order_uuid;
        public string essay;
        public static MsgPack.MessagePackObjectDictionary order_essay_block_to_protcol(order_essay_block _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("order_uuid", _struct.order_uuid);
            _protocol.Add("essay", _struct.essay);
            return _protocol;
        }
        public static order_essay_block protcol_to_order_essay_block(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _structc30f5ed5_c62b_3412_9994_8a91efba785a = new order_essay_block();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "order_uuid"){
                    _structc30f5ed5_c62b_3412_9994_8a91efba785a.order_uuid = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "essay"){
                    _structc30f5ed5_c62b_3412_9994_8a91efba785a.essay = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
            }
            return _structc30f5ed5_c62b_3412_9994_8a91efba785a;
        }
    }

    public class essay_block
    {
        public Int64 img_guid;
        public string img_url;
        public string essay;
        public static MsgPack.MessagePackObjectDictionary essay_block_to_protcol(essay_block _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("img_guid", _struct.img_guid);
            _protocol.Add("img_url", _struct.img_url);
            _protocol.Add("essay", _struct.essay);
            return _protocol;
        }
        public static essay_block protcol_to_essay_block(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct7cba7842_7750_310c_89b9_e3755aad0634 = new essay_block();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "img_guid"){
                    _struct7cba7842_7750_310c_89b9_e3755aad0634.img_guid = ((MsgPack.MessagePackObject)i.Value).AsInt64();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "img_url"){
                    _struct7cba7842_7750_310c_89b9_e3755aad0634.img_url = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "essay"){
                    _struct7cba7842_7750_310c_89b9_e3755aad0634.essay = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
            }
            return _struct7cba7842_7750_310c_89b9_e3755aad0634;
        }
    }

    public class user_img_db
    {
        public string account;
        public UInt64 create_time;
        public Int64 essay_guid;
        public List<essay_block> free_essay;
        public List<essay_block> pay_essay;
        public save_image_state save_state;
        public List<string> prompt;
        public UInt32 hotspot;
        public UInt32 discuss_count;
        public static MsgPack.MessagePackObjectDictionary user_img_db_to_protcol(user_img_db _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("account", _struct.account);
            _protocol.Add("create_time", _struct.create_time);
            _protocol.Add("essay_guid", _struct.essay_guid);
            if (_struct.free_essay != null) {
                var _array_free_essay = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.free_essay){
                    _array_free_essay.Add( new MsgPack.MessagePackObject(essay_block.essay_block_to_protcol(v_)));
                }
                _protocol.Add("free_essay", new MsgPack.MessagePackObject(_array_free_essay));
            }
            if (_struct.pay_essay != null) {
                var _array_pay_essay = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.pay_essay){
                    _array_pay_essay.Add( new MsgPack.MessagePackObject(essay_block.essay_block_to_protcol(v_)));
                }
                _protocol.Add("pay_essay", new MsgPack.MessagePackObject(_array_pay_essay));
            }
            _protocol.Add("save_state", (Int32)_struct.save_state);
            if (_struct.prompt != null) {
                var _array_prompt = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.prompt){
                    _array_prompt.Add(v_);
                }
                _protocol.Add("prompt", new MsgPack.MessagePackObject(_array_prompt));
            }
            _protocol.Add("hotspot", _struct.hotspot);
            _protocol.Add("discuss_count", _struct.discuss_count);
            return _protocol;
        }
        public static user_img_db protcol_to_user_img_db(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct5e54df85_ee72_3562_92b4_6281947c517e = new user_img_db();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "account"){
                    _struct5e54df85_ee72_3562_92b4_6281947c517e.account = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "create_time"){
                    _struct5e54df85_ee72_3562_92b4_6281947c517e.create_time = ((MsgPack.MessagePackObject)i.Value).AsUInt64();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "essay_guid"){
                    _struct5e54df85_ee72_3562_92b4_6281947c517e.essay_guid = ((MsgPack.MessagePackObject)i.Value).AsInt64();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "free_essay"){
                    _struct5e54df85_ee72_3562_92b4_6281947c517e.free_essay = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct5e54df85_ee72_3562_92b4_6281947c517e.free_essay.Add(essay_block.protcol_to_essay_block(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "pay_essay"){
                    _struct5e54df85_ee72_3562_92b4_6281947c517e.pay_essay = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct5e54df85_ee72_3562_92b4_6281947c517e.pay_essay.Add(essay_block.protcol_to_essay_block(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "save_state"){
                    _struct5e54df85_ee72_3562_92b4_6281947c517e.save_state = (save_image_state)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "prompt"){
                    _struct5e54df85_ee72_3562_92b4_6281947c517e.prompt = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct5e54df85_ee72_3562_92b4_6281947c517e.prompt.Add(((MsgPack.MessagePackObject)v_).AsString());
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "hotspot"){
                    _struct5e54df85_ee72_3562_92b4_6281947c517e.hotspot = ((MsgPack.MessagePackObject)i.Value).AsUInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "discuss_count"){
                    _struct5e54df85_ee72_3562_92b4_6281947c517e.discuss_count = ((MsgPack.MessagePackObject)i.Value).AsUInt32();
                }
            }
            return _struct5e54df85_ee72_3562_92b4_6281947c517e;
        }
    }

    public class hotspot_prompt_db
    {
        public string prompt;
        public UInt32 count;
        public static MsgPack.MessagePackObjectDictionary hotspot_prompt_db_to_protcol(hotspot_prompt_db _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("prompt", _struct.prompt);
            _protocol.Add("count", _struct.count);
            return _protocol;
        }
        public static hotspot_prompt_db protcol_to_hotspot_prompt_db(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _struct680b66db_fe6e_3edd_90f4_9082f0b3400e = new hotspot_prompt_db();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "prompt"){
                    _struct680b66db_fe6e_3edd_90f4_9082f0b3400e.prompt = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "count"){
                    _struct680b66db_fe6e_3edd_90f4_9082f0b3400e.count = ((MsgPack.MessagePackObject)i.Value).AsUInt32();
                }
            }
            return _struct680b66db_fe6e_3edd_90f4_9082f0b3400e;
        }
    }

    public class user_img
    {
        public string creator;
        public string creator_name;
        public UInt64 create_time;
        public Int64 essay_guid;
        public List<essay_block> essay;
        public List<string> prompt;
        public UInt32 hotspot;
        public UInt32 discuss_count;
        public static MsgPack.MessagePackObjectDictionary user_img_to_protcol(user_img _struct){
            if (_struct == null)
            {
                return null;
            }
            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("creator", _struct.creator);
            _protocol.Add("creator_name", _struct.creator_name);
            _protocol.Add("create_time", _struct.create_time);
            _protocol.Add("essay_guid", _struct.essay_guid);
            if (_struct.essay != null) {
                var _array_essay = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.essay){
                    _array_essay.Add( new MsgPack.MessagePackObject(essay_block.essay_block_to_protcol(v_)));
                }
                _protocol.Add("essay", new MsgPack.MessagePackObject(_array_essay));
            }
            if (_struct.prompt != null) {
                var _array_prompt = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.prompt){
                    _array_prompt.Add(v_);
                }
                _protocol.Add("prompt", new MsgPack.MessagePackObject(_array_prompt));
            }
            _protocol.Add("hotspot", _struct.hotspot);
            _protocol.Add("discuss_count", _struct.discuss_count);
            return _protocol;
        }
        public static user_img protcol_to_user_img(MsgPack.MessagePackObjectDictionary _protocol){
            if (_protocol == null)
            {
                return null;
            }
            var _structbf3954b2_9b22_32b1_ac84_d4dc1409d485 = new user_img();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "creator"){
                    _structbf3954b2_9b22_32b1_ac84_d4dc1409d485.creator = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "creator_name"){
                    _structbf3954b2_9b22_32b1_ac84_d4dc1409d485.creator_name = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "create_time"){
                    _structbf3954b2_9b22_32b1_ac84_d4dc1409d485.create_time = ((MsgPack.MessagePackObject)i.Value).AsUInt64();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "essay_guid"){
                    _structbf3954b2_9b22_32b1_ac84_d4dc1409d485.essay_guid = ((MsgPack.MessagePackObject)i.Value).AsInt64();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "essay"){
                    _structbf3954b2_9b22_32b1_ac84_d4dc1409d485.essay = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _structbf3954b2_9b22_32b1_ac84_d4dc1409d485.essay.Add(essay_block.protcol_to_essay_block(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "prompt"){
                    _structbf3954b2_9b22_32b1_ac84_d4dc1409d485.prompt = new();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _structbf3954b2_9b22_32b1_ac84_d4dc1409d485.prompt.Add(((MsgPack.MessagePackObject)v_).AsString());
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "hotspot"){
                    _structbf3954b2_9b22_32b1_ac84_d4dc1409d485.hotspot = ((MsgPack.MessagePackObject)i.Value).AsUInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "discuss_count"){
                    _structbf3954b2_9b22_32b1_ac84_d4dc1409d485.discuss_count = ((MsgPack.MessagePackObject)i.Value).AsUInt32();
                }
            }
            return _structbf3954b2_9b22_32b1_ac84_d4dc1409d485;
        }
    }

/*this module code is codegen by abelkhan codegen for c#*/

}
