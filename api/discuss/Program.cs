using abelkhan;
using service;

namespace discuss
{
    class discuss
    {
        static void Main(string[] args)
		{
            var _hub = new hub.hub(args[0], args[1], "discuss");

            _hub.onCloseServer += () => {
                _hub.closeSvr();
            };

            log.log.trace("login start ok");

            _hub.run();
        }
    }
}
