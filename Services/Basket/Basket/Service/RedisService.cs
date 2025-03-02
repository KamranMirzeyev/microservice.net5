﻿using StackExchange.Redis;

namespace Basket.Service
{
    public class RedisService
    {
        private readonly string _host;
        private readonly string _port;

        private ConnectionMultiplexer _connectionMultiplexer;
        public RedisService(string host,string port)
        {
            _host = host;
            _port = port;
        }


        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(db);

    }
}
