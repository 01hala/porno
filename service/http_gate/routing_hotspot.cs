using abelkhan;
using Microsoft.AspNetCore.Http;
using MsgPack;
using MsgPack.Serialization;
using service;
using System.Threading.Tasks;

namespace gate
{
    class routing_hotspot
    {
        private readonly MessagePackSerializer<user_query_prompt_req> _serializer_query_prompt_req = MessagePackSerializer.Get<user_query_prompt_req>();
        private readonly MessagePackSerializer<MessagePackObjectDictionary> _serializer_query_img_rsp = MessagePackSerializer.Get<MessagePackObjectDictionary>();

        public routing_hotspot()
        {
            HttpService.post("/query_prompt", query_prompt);
        }

        private async Task query_prompt(AbelkhanHttpRequest req)
        {
            try
            {
                using var st = MemoryStreamPool.mstMgr.GetStream();
                st.Write(req.Content);
                st.Position = 0;
                var _req = _serializer_query_prompt_req.Unpack(st);

                var rsp = await gate._hotspot_mgr.random_hotsport_proxy().query_prompt(_req.prompt, _req.current_img_page);
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
