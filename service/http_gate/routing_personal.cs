using abelkhan;
using Microsoft.AspNetCore.Http;
using MsgPack;
using MsgPack.Serialization;
using service;
using System.Threading.Tasks;

namespace gate
{
    class routing_personal
    {
        private readonly MessagePackSerializer<user_query_personal_req> _serializer_query_personal_req = MessagePackSerializer.Get<user_query_personal_req>();
        private readonly MessagePackSerializer<MessagePackObjectDictionary> _serializer_query_img_rsp = MessagePackSerializer.Get<MessagePackObjectDictionary>();

        public routing_personal()
        {
            HttpService.post("/query_personal", query_personal);
        }

        private async Task query_personal(AbelkhanHttpRequest req)
        {
            try
            {
                using var st = MemoryStreamPool.mstMgr.GetStream();
                st.Write(req.Content);
                st.Position = 0;
                var _req = _serializer_query_personal_req.Unpack(st);

                string user_account = await gate._redis_handle.GetStrData(redis_help.BuildUserTokenAccountKey(_req.token));
                var is_self = user_account == _req.personal;
                var rsp = await gate._personal_mgr.random_personal_proxy().query_personal(is_self, _req.personal, _req.last_img_guid);
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
