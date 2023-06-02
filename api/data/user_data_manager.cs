using abelkhan;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hub;
using System.Text;
using System.Security.Principal;
using System.Xml.Linq;

namespace data
{
    public class user_data
    {
        public string type;
        public string account;
        public string mail;
        public string password;
        public string name;
        public int coin;
        public int level;
        public List<string> follows = new();
    }

    class user_proxy
    {
        public const string USER_TYPE_GUEST = "guest";
        public const string USER_TYPE_USER = "user";

        public user_data data;
        public string token;
        public long timeout = timerservice.Tick + constant.constant.UserTimeoutTmp;
        public abelkhan.user_data ProtoData
        {
            get
            {
                var _data = new abelkhan.user_data()
                {
                    account = data.account,
                    name = data.name,
                    coin = data.coin,
                    level = data.level,
                };
                return _data;
            }
        }

        private string bind_db_proxy_name = null;
        public dbproxyproxy.Collection PlayerCollection
        {
            get
            {
                dbproxyproxy bind_db_proxy;
                do
                {
                    if (string.IsNullOrEmpty(bind_db_proxy_name))
                    {
                        bind_db_proxy = hub.hub.get_random_dbproxyproxy();
                        bind_db_proxy_name = bind_db_proxy.db_name;
                        break;
                    }

                    bind_db_proxy = hub.hub.get_dbproxy(bind_db_proxy_name);
                    if (bind_db_proxy == null)
                    {
                        bind_db_proxy = hub.hub.get_random_dbproxyproxy();
                        bind_db_proxy_name = bind_db_proxy.db_name;
                    }

                } while (false);

                return bind_db_proxy.getCollection(constant.constant.PornoDBName, constant.constant.UserDBCollection);
            }
        }

        public user_proxy(user_data _data, string _token)
        {
            data = _data;
            token = _token;
        }

        public Task<bool> update_account(string account, string password, string _mail, string name)
        {
            var task = new TaskCompletionSource<bool>();

            var tmp_data = new user_data()
            {
                type = user_proxy.USER_TYPE_USER,
                account = account,
                mail = _mail,
                password = password,
                name = name,
                coin = data.coin,
                level = data.level
            };
            var query = new DBQueryHelper();
            query.condition("account", data.account);
            var update = new UpdateDataHelper();
            update.set(tmp_data);
            PlayerCollection.updataPersistedObject(query.query(), update.data(), false, (result) =>
            {
                if (result == hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                {
                    data = tmp_data;
                    task.SetResult(true);
                }
                else
                {
                    log.log.err("update_account faild:{0}", result.ToString());
                    task.SetResult(false);
                }
            });

            return task.Task;
        }   
    }

    class user_data_manager
    {
        private readonly Dictionary<string, user_proxy> account_user = new();
        private readonly Dictionary<string, user_proxy> token_user = new();

        private hub.dbproxyproxy.Collection Collection
        {
            get
            {
                return hub.hub.get_random_dbproxyproxy().getCollection(constant.constant.PornoDBName, constant.constant.UserDBCollection);
            }
        }

        private hub.dbproxyproxy.Collection GuestGuidCollection
        {
            get
            {
                return hub.hub.get_random_dbproxyproxy().getCollection(constant.constant.PornoDBName, constant.constant.GuestGuidCollection);
            }
        }

        private class DBException : System.Exception
        {
            public string err_info;
            public DBException(string err)
            {
                err_info = err;
            }
        }

        public user_data_manager()
        {
            hub.hub._timer.addticktime(10 * 60 * 1000, tick_user_proxy);
        }

        private void tick_user_proxy(long tick)
        {
            try
            {
                var remove_user = new List<user_proxy>();
                foreach(var (_, _proxy) in token_user)
                {
                    if (_proxy.timeout < timerservice.Tick)
                    {
                        remove_user.Add(_proxy);
                    }
                }

                foreach(var _proxy in remove_user)
                {
                    token_user.Remove(_proxy.token);
                    account_user.Remove(_proxy.data.account);

                    data._redis_handle.DelData(redis_help.BuildUserTokenAccountKey(_proxy.token));
                    data._redis_handle.DelData(redis_help.BuildUserDataCacheKey(_proxy.data.account));
                    data._redis_handle.DelData(redis_help.BuildUserAccountNameKey(_proxy.data.account));
                }
            }
            catch(System.Exception ex)
            {
                log.log.err("tick_user_proxy ex:{0}", ex);
            }
            finally
            {
                hub.hub._timer.addticktime(10 * 60 * 1000, tick_user_proxy);
            }
        }

        private Task<long> get_guest_guid()
        {
            var task = new TaskCompletionSource<long>();

            GuestGuidCollection.getGuid(constant.constant.InsideGuid, (_result, guid) =>
            {
                if (_result == hub.dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                {
                    task.SetResult(guid);
                }
                else
                {
                    task.SetException(new DBException($"get guid err:{_result}"));
                }
            });

            return task.Task;
        }

        public async Task<user_proxy> create_guest_data()
        {
            var _account = Guid.NewGuid().ToString("N");
            var token = Guid.NewGuid().ToString("N");
            var guid = await get_guest_guid();

            var user_data = new user_data
            {
                type = user_proxy.USER_TYPE_GUEST,
                account = _account,
                name = $"guest{guid}",
                coin = 0,
                level = 0
            };
            Collection.createPersistedObject(user_data.ToBsonDocument(), (_result) => {
                if (_result != dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                {
                    log.log.err($"createPersistedObject error, user type:{user_data.type}, account:{_account}, name:{user_data.name}, err:{_result}");
                }
            });

            var _proxy = new user_proxy(user_data, token);
            account_user[_account] = _proxy;
            token_user[token] = _proxy;

            await data._redis_handle.SetStrData(redis_help.BuildUserTokenAccountKey(token), _account);

            return _proxy;
        }

        public async Task<user_proxy> create_use_data(string account, string password, string _mail, string name)
        {
            var token = Guid.NewGuid().ToString("N");

            var user_data = new user_data
            {
                type = user_proxy.USER_TYPE_USER,
                account = account,
                mail = _mail,
                password = password,
                name = name,
                coin = 0,
                level = 0
            };
            Collection.createPersistedObject(user_data.ToBsonDocument(), (_result) => {
                if (_result != dbproxyproxy.EM_DB_RESULT.EM_DB_SUCESSED)
                {
                    log.log.err($"createPersistedObject error, user type:{user_data.type}, account:{account}, name:{user_data.name}, err:{_result}");
                }
            });

            var _proxy = new user_proxy(user_data, token);
            account_user[account] = _proxy;
            token_user[token] = _proxy;

            await data._redis_handle.SetStrData(redis_help.BuildUserTokenAccountKey(token), account);
            await data._redis_handle.SetStrData(redis_help.BuildUserAccountNameKey(account), name);

            return _proxy;
        }

        public user_proxy get_user_proxy_by_token(string token)
        {
            token_user.TryGetValue(token, out user_proxy _proxy);
            if (_proxy != null)
            {
                _proxy.timeout = timerservice.Tick + constant.constant.UserTimeoutTmp;
            }
            return _proxy;
        }

        private Task<user_proxy> load_user_proxy(string account)
        {
            var task = new TaskCompletionSource<user_proxy>();

            var query = new DBQueryHelper();
            query.condition("account", account);
            Collection.getObjectInfo(query.query(), async (_data) =>
            {
                if (_data.Count == 0)
                {
                    task.SetResult(null);
                }
                else if (_data.Count == 1)
                {
                    var _user_data = BsonSerializer.Deserialize<user_data>(_data[0] as BsonDocument);
                    var _token = Guid.NewGuid().ToString("N");

                    var _proxy = new user_proxy(_user_data, _token);
                    account_user[account] = _proxy;
                    token_user[_token] = _proxy;

                    await data._redis_handle.SetStrData(redis_help.BuildUserTokenAccountKey(_token), account);
                    await data._redis_handle.SetStrData(redis_help.BuildUserAccountNameKey(account), _user_data.name);

                    task.SetResult(_proxy);
                }
                else
                {
                    task.SetException(new DBException("data more then 1"));
                }
            }, () =>
            {
            });

            return task.Task;
        }

        public async Task<user_proxy> get_user_proxy_by_account(string account)
        {
            account_user.TryGetValue(account, out user_proxy _proxy);
            if (_proxy != null)
            {
                _proxy.timeout = timerservice.Tick + constant.constant.UserTimeoutTmp;
            }
            else
            {
                return await load_user_proxy(account);
            }
            return _proxy;
        }
    }
}
