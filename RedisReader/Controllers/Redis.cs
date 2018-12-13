using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisReader.Controllers
{
    public class Redis
    {
        public Redis()
        {
        }
        public static ConnectionMultiplexer conn;
        public static ConnectionMultiplexer GetInstance(string host, int port)
        {
            if (conn == null)
            {
                var configOptions = new StackExchange.Redis.ConfigurationOptions
                {
                    ConnectTimeout = 5000,
                    ConnectRetry = 5,
                    SyncTimeout = 5000,
                    AbortOnConnectFail = false,
                };
                configOptions.EndPoints.Add(host, port);
                conn = ConnectionMultiplexer.Connect(configOptions);
            }
            return conn;
        }


    }
}