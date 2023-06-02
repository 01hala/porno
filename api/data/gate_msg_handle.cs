using abelkhan;
using MongoDB.Bson;
using service;
using System;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace data
{
    struct check_account
    {
        public string account;
        public string token;
    }

    struct check_mail
    {
        public string mail;
        public string token;
    }

    class gate_msg_handle
    {
        private hub.dbproxyproxy.Collection CheckAccountCollection
        {
            get
            {
                return hub.hub.get_random_dbproxyproxy().getCollection(constant.constant.PornoDBName, constant.constant.CheckAccountDBCollection);
            }
        }

        private hub.dbproxyproxy.Collection CheckMailCollection
        {
            get
            {
                return hub.hub.get_random_dbproxyproxy().getCollection(constant.constant.PornoDBName, constant.constant.CheckMailDBCollection);
            }
        }

        private gate_data_module _module = new();
        public gate_msg_handle()
        {
            _module.on_guest_login += _module_on_guest_login;
            _module.on_user_login += _module_on_user_login;
            _module.on_user_create += _module_on_user_create;
            _module.on_payment_verify += _module_on_payment_verify;
            _module.on_get_user_follow += _module_on_get_user_follow;
        }

        private void _module_on_get_user_follow(string token)
        {
            log.log.trace("on_payment_verify begin!");

            var rsp = (gate_data_get_user_follow_rsp)_module.rsp;
            var _proxy = data._user_mgr.get_user_proxy_by_token(token);
            if (_proxy != null)
            {
                rsp.rsp(_proxy.data.follows);
            }
            else
            {
                rsp.err(error.no_exist_user);
            }
        }

        private void _module_on_payment_verify(payment _pay_info)
        {
            log.log.trace("on_payment_verify begin!");

            var rsp = (gate_data_payment_verify_rsp)_module.rsp;
            var _proxy = data._user_mgr.get_user_proxy_by_token(_pay_info.token);
            if (_proxy != null)
            {
                if (_pay_info.type == coin_type.coin)
                {
                    var expected_price = constant.constant.CoinPrice * (100 - _proxy.data.level) / 100;
                    if (_proxy.data.coin > expected_price)
                    {
                        _proxy.data.coin -= expected_price;
                        rsp.rsp();
                    }
                    else
                    {
                        rsp.err(error.no_enough_coin);
                    }
                }
                else
                {
                    var expected_price = constant.constant.MoneyPrice * (100 - _proxy.data.level) / 100;
                    if (expected_price <= _pay_info.number)
                    {
                        rsp.rsp();
                    }
                    else
                    {
                        rsp.err(error.money_number_mismatch);
                    }
                }
            }
        }

        private Task<bool> check_account(string _token, string _account)
        {
            var task = new TaskCompletionSource<bool>();

            var doc = new check_account()
            {
                account = _account,
                token = _token,
            };
            CheckAccountCollection.createPersistedObject(doc.ToBsonDocument(), (result) =>
            {
                if (result == hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                {
                    task.SetResult(true);
                }
                else
                {
                    task.SetResult(false);
                }
            });

            return task.Task;
        }

        private Task rollback_account(string _token)
        {
            var task = new TaskCompletionSource();

            var query = new DBQueryHelper();
            query.condition(constant.constant.CheckDBPrimaryKey, _token);
            CheckAccountCollection.removeObject(query.query(), (result) =>
            {
                if (result != hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                {
                    log.log.err("rollback_account faild:{0}", result.ToString());
                }
                task.SetResult();
            });

            return task.Task;
        }

        private Task<bool> check_mail(string _token, string _mail)
        {
            var task = new TaskCompletionSource<bool>();

            var doc = new check_mail()
            {

                mail = _mail,
                token = _token,
            };
            CheckMailCollection.createPersistedObject(doc.ToBsonDocument(), (result) =>
            {
                if (result == hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                {
                    task.SetResult(true);
                }
                else
                {
                    task.SetResult(false);
                }
            });

            return task.Task;
        }

        private Task rollback_mail(string _token)
        {
            var task = new TaskCompletionSource();

            var query = new DBQueryHelper();
            query.condition(constant.constant.CheckDBPrimaryKey, _token);
            CheckMailCollection.removeObject(query.query(), (result) =>
            {
                if (result != hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                {
                    log.log.err("rollback_account faild:{0}", result.ToString());
                }
                task.SetResult();
            });

            return task.Task;
        }

        private async void _module_on_user_create(string token, string account, string password, string mail, string name)
        {
            log.log.trace("on_user_create begin!");

            var rsp = (gate_data_user_create_rsp)_module.rsp;
            try
            {
                var _check_token = Guid.NewGuid().ToString("N");
                if (!(await check_account(_check_token, account)))
                {
                    rsp.err(error.account_repeat);
                    return;
                }
                if (!(await check_mail(_check_token, mail)))
                {
                    await rollback_account(_check_token);
                    rsp.err(error.mail_repeat);
                    return;
                }

                if (string.IsNullOrEmpty(token))
                {
                    var _proxy = await data._user_mgr.create_use_data(account, password, mail, name);
                    rsp.rsp(_proxy.ProtoData, _proxy.token);
                }
                else
                {
                    var _proxy = data._user_mgr.get_user_proxy_by_token(token);
                    if (await _proxy.update_account(account, password, mail, name))
                    {
                        rsp.rsp(_proxy.ProtoData, _proxy.token);
                    }
                    else
                    {
                        await rollback_account(_check_token); 
                        await rollback_mail(_check_token);

                        rsp.err(error.db_error);
                    }
                }
            }
            catch (System.Exception ex)
            {
                log.log.err("on_user_create ex:{0}", ex);
                rsp.err(error.db_error);
            }
        }

        private async void _module_on_user_login(string account, string password)
        {
            log.log.trace("on_user_login begin!");

            var rsp = (gate_data_user_login_rsp)_module.rsp;

            try
            {
                var _proxy = await data._user_mgr.get_user_proxy_by_account(account);
                if (_proxy != null)
                {
                    if (_proxy.data.password == password)
                    {
                        rsp.rsp(_proxy.ProtoData, _proxy.token);
                    }
                    else
                    {
                        rsp.err(error.wrong_password);
                    }
                }
                else
                {
                    rsp.err(error.no_exist_user);
                }
            }
            catch (System.Exception ex)
            {
                log.log.err("on_user_login ex:{0}", ex);
                rsp.err(error.db_error);
            }
        }

        private async void _module_on_guest_login(string account)
        {
            log.log.trace("on_guest_login begin!");

            var rsp = (gate_data_guest_login_rsp)_module.rsp;

            try
            {
                var _proxy = await data._user_mgr.get_user_proxy_by_account(account);
                if (_proxy == null)
                {
                    _proxy = await data._user_mgr.create_guest_data();
                }

                rsp.rsp(_proxy.ProtoData, _proxy.token);
            }
            catch (System.Exception ex)
            {
                log.log.err("on_guest_login ex:{0}", ex);
                rsp.err(error.db_error);
            }
        }
    }
}
