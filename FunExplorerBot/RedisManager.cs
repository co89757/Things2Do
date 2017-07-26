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
            "";

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(
            () => ConnectionMultiplexer.Connect(ConnectionString)
             );

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        public IDatabase Cache => RedisManager.Connection.GetDatabase();

        public bool TryGet(string key, out string value)
        {
            bool exist = true;
            var v = this.Cache.StringGet(key);
            if (v == RedisValue.Null)
            {
                exist = false;
            }
            value = v;
            return exist;
        }

    }
}