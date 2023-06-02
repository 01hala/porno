using abelkhan;
using hub;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gate
{
    public class gen_proxy
    {
        private static gate_gen_caller gate_Gen_Caller = new();

        public static Task<string> save_img(string gen_svr_name, string account, string order_uuid, save_image_state state)
        {
            var task = new TaskCompletionSource<string>();

            gate_Gen_Caller.get_hub(gen_svr_name).save_img(account, order_uuid, state).callBack((url) =>
            {
                task.SetResult(url);
            }, (err) =>
            {
                log.log.err("save img error:{0}", err);
                task.SetResult(string.Empty);
            }).timeout(3000, () =>
            {
                log.log.err("save img timeout!");
                task.SetResult(string.Empty);
            });

            return task.Task;
        }
    }
}
