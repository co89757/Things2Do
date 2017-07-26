using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StackExchange.Redis;
namespace FunExplorerBot
{
    public class RedisManager
    {

        public const string ConnectionString =
            "colindev.redis.cache.windows.net:6380,password=IyAlWWKdWV0hQFg7w5zIaity106BK5foxrL4f2k0mp0=,ssl=True,abortConnect=False";

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(
            () => ConnectionMultiplexer.Connect(ConnectionString)
             );

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        public IDatabase Db => RedisManager.Connection.GetDatabase();

        public bool TryGet(string key, out string value)
        {
            bool exist = true;
            var v = this.Db.StringGet(key);
            if (v == RedisValue.Null)
            {
                exist = false;
            }
            value = v;
            return exist;
        }

    }
}