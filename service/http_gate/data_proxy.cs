using abelkhan;
using hub;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gate
{
    class guest_login_exception : System.Exception
    {
        public error err_code;

        public guest_login_exception(error err)
        {
            err_code = err;
        }
    }

    class data_proxy
    {
        private readonly gate_data_caller gate_data_caller;
        private readonly hubproxy proxy;

        public data_proxy(gate_data_caller _gate_data_caller, hubproxy _proxy)
        {
            gate_data_caller = _gate_data_caller;
            proxy = _proxy;
        }

        public Task<List<string> > get_user_follow(string token)
        {
            var task = new TaskCompletionSource<List<string> >();

            gate_data_caller.get_hub(proxy.name).get_user_follow(token).callBack((follow_list) =>
            {
                task.SetResult(follow_list);
            }, (err) =>
            {
                log.log.err("get_user_follow err:{0}", err);
            }).timeout(3000, () =>
            {
                log.log.err("get_user_follow timeout!");
            });

            return task.Task;
        }

        public Task<user_data_rsp> guest_login(string account)
        {
            var task = new TaskCompletionSource<user_data_rsp>();
            var rsp = new user_data_rsp();

            gate_data_caller.get_hub(proxy.name).guest_login(account).callBack((_data, _token) =>
            {
                rsp.data = _data;
                rsp.token = _token;
                task.SetResult(rsp);

            }, (err) =>
            {
                rsp.code = err;
                task.SetResult(rsp);

            }).timeout(3000, () =>
            {
                rsp.code = error.timeout;
                task.SetResult(rsp);
            });

            return task.Task;
        }

        public Task<user_data_rsp> user_login(string account, string password)
        {
            var task = new TaskCompletionSource<user_data_rsp>();
            var rsp = new user_data_rsp();

            gate_data_caller.get_hub(proxy.name).user_login(account, password).callBack((_data, _token) =>
            {
                rsp.data = _data;
                rsp.token = _token;
                task.SetResult(rsp);

            }, (err) =>
            {
                rsp.code = err;
                task.SetResult(rsp);

            }).timeout(3000, () =>
            {
                rsp.code = error.timeout;
                task.SetResult(rsp);
            });

            return task.Task;
        }

        public Task<user_data_rsp> user_create(string token, string account, string password, string mail, string name)
        {
            var task = new TaskCompletionSource<user_data_rsp>();
            var rsp = new user_data_rsp();

            gate_data_caller.get_hub(proxy.name).user_create(token, account, password, mail, name).callBack((_data, _token) =>
            {
                rsp.data = _data;
                rsp.token = _token;
                task.SetResult(rsp);

            }, (err) =>
            {
                rsp.code = err;
                task.SetResult(rsp);

            }).timeout(3000, () =>
            {
                rsp.code = error.timeout;
                task.SetResult(rsp);
            });

            return task.Task;
        }

        public Task<error> payment_verify(string _token, coin_type _type, int _number)
        {
            var task = new TaskCompletionSource<error>();

            var _info = new payment()
            {
                token = _token,
                type = _type,
                number = _number,
            };
            gate_data_caller.get_hub(proxy.name).payment_verify(_info).callBack(() =>
            {
                task.SetResult(error.success);
            }, (err) =>
            {
                log.log.err("payment verify faild, err_code:{0}!", err);
                task.SetResult(err);
            }).timeout(1000, () => {
                log.log.err("payment verify timeout!");
                task.SetResult(error.timeout);
            });

            return task.Task;
        }
    }

    class data_manager
    {
        private readonly gate_data_caller gate_data_caller = new ();
        private readonly Dictionary<string, data_proxy> datas = new ();

        public data_manager()
        {
        }

        public void on_data_proxy(hubproxy _proxy)
        {
            if (_proxy.type == "data")
            {
                var _data_proxy = new data_proxy(gate_data_caller, _proxy);
                datas[_proxy.name] = _data_proxy;
            }
        }

        public bool get_data_proxy(string data_name, out data_proxy _proxy)
        {
            return datas.TryGetValue(data_name, out _proxy);
        }

        public data_proxy random_data_proxy()
        {
            var data_list = datas.Values.ToArray();
            if (data_list.Count() > 0)
            {
                return data_list[RandomHelper.RandomInt(data_list.Count())];
            }
            return null;
        }
    }
}
