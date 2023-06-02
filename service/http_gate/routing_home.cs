using abelkhan;
using Microsoft.AspNetCore.Http;
using MsgPack;
using MsgPack.Serialization;
using service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gate
{
    class routing_home
    {
        private readonly MessagePackSerializer<user_query_home_req> _serializer_query_home_req = MessagePackSerializer.Get<user_query_home_req>();
        private readonly MessagePackSerializer<MessagePackObjectDictionary> _serializer_query_img_rsp = MessagePackSerializer.Get<MessagePackObjectDictionary>();

        public routing_home()
        {
            HttpService.post("/query_home", query_home);
        }

        private async Task query_home(AbelkhanHttpRequest req)
        {
            try
            {
                using var st = MemoryStreamPool.mstMgr.GetStream();
                st.Write(req.Content);
                st.Position = 0;
                var _req = _serializer_query_home_req.Unpack(st);

                string user_account = await gate._redis_handle.GetStrData(redis_help.BuildUserTokenAccountKey(_req.token));
                string data_name = await gate._redis_handle.GetStrData(redis_help.BuildUserDataCacheKey(user_account));
                if (string.IsNullOrEmpty(data_name) || !gate._data_mgr.get_data_proxy(data_name, out data_proxy _proxy))
                {
                    _proxy = gate._data_mgr.random_data_proxy();
                }
                var follow_list = await _proxy.get_user_follow(_req.token);
                follow_list.Add(user_account);

                var current_img_guid = _req.last_img_guid;
                var rsp_img_list = new SortedList<long, user_img>();
                foreach (var _account in follow_list)
                {
                    var is_self = _account == user_account;
                    var _tmp_rsp = await gate._personal_mgr.random_personal_proxy().query_personal(is_self, _account, _req.last_img_guid);
                    foreach (var _img in _tmp_rsp.img_list)
                    {
                        if (current_img_guid > _img.essay_guid)
                        {
                            current_img_guid = _img.essay_guid;
                        }
                        rsp_img_list.Add(_img.essay_guid, _img);
                    }
                }

                var tmp_img = rsp_img_list.Values.ToList();
                var count = tmp_img.Count;
                count = count > constant.constant.ImagePageCount ? constant.constant.ImagePageCount : count;
                tmp_img = tmp_img.GetRange(0, count);

                var rsp = new user_query_img_rsp()
                {
                    code = error.success,
                    current_img_guid = current_img_guid,
                    img_list = tmp_img,
                };
                using var st_rsp = MemoryStreamPool.mstMgr.GetStream();
                _serializer_query_img_rsp.Pack(st_rsp, user_query_img_rsp.user_query_img_rsp_to_protcol(rsp));
                st_rsp.Position = 0;
                await req.Response(StatusCodes.Status200OK, HttpService.buildCrossHeaders(), st_rsp.ToArray());
            }
            catch (System.Exception ex)
            {
                log.log.err("query_prompt ex:{0}", ex);
            }
        }
    }
}
