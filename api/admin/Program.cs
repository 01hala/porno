using abelkhan;
using service;

namespace admin
{
    class admin
    {
        static void Main(string[] args)
		{
            var _hub = new hub.hub(args[0], args[1], "admin");

            _hub.onCloseServer += () => {
                _hub.closeSvr();
            };

            log.log.trace("login start ok");

            _hub.run();
        }
    }
}
