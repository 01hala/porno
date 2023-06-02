using abelkhan;
using Microsoft.AspNetCore.Http;
using MsgPack;
using MsgPack.Serialization;
using service;
using System.Threading.Tasks;

namespace gate
{
    class routing_login
    {
        private readonly MessagePackSerializer<guest_req> _serializer_guest_req = MessagePackSerializer.Get<guest_req>();
        private readonly MessagePackSerializer<user_login_req> _serializer_user_login_req = MessagePackSerializer.Get<user_login_req>();
        private readonly MessagePackSerializer<user_create_req> _serializer_user_create_req = MessagePackSerializer.Get<user_create_req>();
        private readonly MessagePackSerializer<MessagePackObjectDictionary> _serializer_user_data_rsp = MessagePackSerializer.Get<MessagePackObjectDictionary>();

        public routing_login()
        {
            HttpService.post("/guest_login", guest_login);
            HttpService.post("/user_login", user_login);
            HttpService.post("/user_create", user_create);
        }

        private async Task user_create(AbelkhanHttpRequest req)
        {
            using var st = MemoryStreamPool.mstMgr.GetStream();
            st.Write(req.Content);
            st.Position = 0;
            var _req = _serializer_user_create_req.Unpack(st);

            try
            {
                data_proxy _proxy = null;
                string user_account = await gate._redis_handle.GetStrData(redis_help.BuildUserTokenAccountKey(_req.token));
                if (!string.IsNullOrEmpty(user_account))
                {
                    string data_name = await gate._redis_handle.GetStrData(redis_help.BuildUserDataCacheKey(user_account));
                    if (string.IsNullOrEmpty(data_name) || !gate._data_mgr.get_data_proxy(data_name, out _proxy))
                    {
                        _proxy = gate._data_mgr.random_data_proxy();
                    }
                }
                var _rsp = await _proxy.user_create(_req.token, _req.account, _req.password, _req.mail, _req.name);
                using var st_rsp = MemoryStreamPool.mstMgr.GetStream();
                _serializer_user_data_rsp.Pack(st_rsp, user_data_rsp.user_data_rsp_to_protcol(_rsp));
                st_rsp.Position = 0;

                await req.Response(StatusCodes.Status200OK, HttpService.buildCrossHeaders(), st_rsp.ToArray());
            }
            catch (System.Exception ex)
            {
                log.log.err("guest_login ex：{0}", ex);
            }
        }

        private async Task user_login(AbelkhanHttpRequest req)
        {
            using var st = MemoryStreamPool.mstMgr.GetStream();
            st.Write(req.Content);
            st.Position = 0;
            var _req = _serializer_user_login_req.Unpack(st);

            try
            {
                data_proxy _proxy = null;
                string data_name = await gate._redis_handle.GetStrData(redis_help.BuildUserDataCacheKey(_req.account));
                if (string.IsNullOrEmpty(data_name) || !gate._data_mgr.get_data_proxy(data_name, out _proxy))
                {
                    _proxy = gate._data_mgr.random_data_proxy();
                }
                var _rsp = await _proxy.user_login(_req.account, _req.password);
                using var st_rsp = MemoryStreamPool.mstMgr.GetStream();
                _serializer_user_data_rsp.Pack(st_rsp, user_data_rsp.user_data_rsp_to_protcol(_rsp));
                st_rsp.Position = 0;

                await req.Response(StatusCodes.Status200OK, HttpService.buildCrossHeaders(), st_rsp.ToArray());
            }
            catch (System.Exception ex)
            {
                log.log.err("guest_login ex：{0}", ex);
            }
        }

        private async Task guest_login(AbelkhanHttpRequest req)
        {
            using var st = MemoryStreamPool.mstMgr.GetStream();
            st.Write(req.Content);
            st.Position = 0;
            var _req = _serializer_guest_req.Unpack(st);

            try
            {
                data_proxy _proxy = null;
                string data_name = await gate._redis_handle.GetStrData(redis_help.BuildUserDataCacheKey(_req.account));
                if (string.IsNullOrEmpty(data_name) || !gate._data_mgr.get_data_proxy(data_name, out _proxy))
                {
                    _proxy = gate._data_mgr.random_data_proxy();
                }
                var _rsp = await _proxy.guest_login(_req.account);
                using var st_rsp = MemoryStreamPool.mstMgr.GetStream();
                _serializer_user_data_rsp.Pack(st_rsp, user_data_rsp.user_data_rsp_to_protcol(_rsp));
                st_rsp.Position = 0;

                await req.Response(StatusCodes.Status200OK, HttpService.buildCrossHeaders(), st_rsp.ToArray());
            }
            catch (System.Exception ex)
            {
                log.log.err("guest_login ex：{0}", ex);
            }
        }
    }
}
