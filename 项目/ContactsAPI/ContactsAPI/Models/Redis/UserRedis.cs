using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.Redis
{
    public class UserRedis : IDisposable
    {
        private string _connectionString;
        private int _defalutDb;
        private string _pwd;
        private string _port;
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;
        private string _instanceName;
        public UserRedis(string connectionString,string instanceName, int defalutDb = 0)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _defalutDb = defalutDb;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
            _instanceName = instanceName;
        }
        private ConnectionMultiplexer GetConnect()
        {
            return _connections.GetOrAdd(_instanceName, p => ConnectionMultiplexer.Connect(_connectionString));
        }
        public IDatabase GetDatabase()
        {
            return GetConnect().GetDatabase(_defalutDb);
        }
        public IServer GetServer(string configName = null, int endPointsIndex = 0)
        {
            var confOption = ConfigurationOptions.Parse(_connectionString);
            return GetConnect().GetServer(confOption.EndPoints[endPointsIndex]);
        }
        public ISubscriber GetSubscriber(string configName = null)
        {
            return GetConnect().GetSubscriber();
        }
        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach (var item in _connections.Values)
                {
                    item.Close();
                }
            }
        }
    }
}
