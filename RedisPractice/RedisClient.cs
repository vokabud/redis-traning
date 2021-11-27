using System;
using StackExchange.Redis;

namespace RedisPractice
{
    internal class RedisClient
    {
        private readonly string Host;
        private readonly string Port;

        public RedisClient(
            string host,
            string port)
        {
            Host = host;
            Port = port;
        }

        public void Set(string key, string value)
            => Connect()
                .GetDatabase()
                .StringSet(
                    key: key,
                    value: new RedisValue(value));

        public string Get(string key)
        {
            using var redis = Connect();
            var database = redis.GetDatabase();

            if (!database.KeyExists(key))
            {
                throw new ArgumentException();
            }

            var value = database
                .StringGet(key: key);

            if (value.HasValue)
            {
                return value;

            }

            return default;
        }

        private ConnectionMultiplexer Connect()
            => ConnectionMultiplexer
                .Connect(
                    new ConfigurationOptions
                    {
                        EndPoints = {$"{Host}:{Port}"}
                    });
    }
}
