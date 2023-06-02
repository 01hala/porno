using abelkhan;
using service;

namespace generate
{
    class generate
    {
        public static redis_handle _redis_handle;

        static void Main(string[] args)
		{
            var _hub = new hub.hub(args[0], args[1], "generate");
            _redis_handle = new redis_handle(hub.hub._root_config.get_value_string("redis_for_cache"));
            var _gen_img = new gen_image(_redis_handle);
            _gen_img.start();

            _hub.onCloseServer += () => {
                _hub.closeSvr();
            };

            var _gate_msg_handle = new gate_msg_handle();

            log.log.trace("login start ok");

            _hub.run();
            _gen_img.close();
        }
    }
}
