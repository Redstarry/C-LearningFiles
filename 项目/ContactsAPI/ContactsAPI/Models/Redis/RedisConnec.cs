using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace ContactsAPI.Models.Redis
{
    public class RedisConnec
    {
        private ConnectionMultiplexer redis;
        private IDatabase db;
        public RedisConnec()
        {
            redis = ConnectionMultiplexer.Connect("192.168.175.130:6379, password = 520weizhuer");
            db = redis.GetDatabase();
        }
        public void demo()
        {
            var a= db.StringGet("Tgd");
            Console.WriteLine(a);
        }
    }
}
