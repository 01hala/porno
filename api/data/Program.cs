using abelkhan;
using service;

namespace data
{
    class data
    {
        public static redis_handle _redis_handle;
        public static readonly user_data_manager _user_mgr = new ();

        static void Main(string[] args)
		{
            var _hub = new hub.hub(args[0], args[1], "data");
            _redis_handle = new redis_handle(hub.hub._root_config.get_value_string("redis_for_cache"));

            _hub.onCloseServer += () => {
                _hub.closeSvr();
            };

            var _gate_msg_handle = new gate_msg_handle();

            log.log.trace("login start ok");

            _hub.run();
        }
    }
}
