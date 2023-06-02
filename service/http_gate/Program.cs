using abelkhan;
using service;

namespace gate
{
    class gate
    {
        public static redis_handle _redis_handle;
        public static data_manager _data_mgr = new();
        public static hotspot_manager _hotspot_mgr = new();
        public static personal_manager _personal_mgr = new();

        static void Main(string[] args)
		{
            var _hub = new hub.hub(args[0], args[1], "http_gate");
            _redis_handle = new redis_handle(hub.hub._root_config.get_value_string("redis_for_cache"));

            _hub.onCloseServer += () => {
                _hub.closeSvr();
            };

            _hub.on_hubproxy += (_proxy) =>
            {
                if (_proxy.type == "data")
                {
                    _data_mgr.on_data_proxy(_proxy);
                }
                else if (_proxy.type == "hotspot")
                {
                    _hotspot_mgr.on_hotspot(_proxy);
                }
                else if (_proxy.type == "personal")
                {
                    _personal_mgr.on_personal(_proxy);
                }
            };
            _hub.on_hubproxy_reconn += (_proxy) =>
            {
                if (_proxy.type == "data")
                {
                    _data_mgr.on_data_proxy(_proxy);
                }
                else if (_proxy.type == "hotspot")
                {
                    _hotspot_mgr.on_hotspot(_proxy);
                }
                else if (_proxy.type == "personal")
                {
                    _personal_mgr.on_personal(_proxy);
                }
            };

            var _login = new routing_login();
            var _home = new routing_home();
            var _payment = new routing_payment();
            var _hotspot = new routing_hotspot();
            var _personal = new routing_personal();

            log.log.trace("login start ok");

            _hub.run();
        }
    }
}
