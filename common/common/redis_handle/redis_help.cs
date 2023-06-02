

using System;
using System.Security.Principal;

namespace abelkhan
{
    public class redis_help
    {
        static public string BuildUserDataCacheKey(string account)
        {
            return $"User:DataCache:{account}";
        }

        static public string BuildUserTokenAccountKey(string token)
        {
            return $"User:TokenAccountCache:{token}";
        }

        static public string BuildUserAccountNameKey(string account)
        {
            return $"User:AccountNameCache:{account}";
        }

        static public string BuildUserOrderCacheLock(string account)
        {
            return $"User:OrderCacheLock:{account}";
        }

        static public string BuildUserOrderCacheKey(string account)
        {
            return $"User:OrderCache:{account}";
        }
    }
}
