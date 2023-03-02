using CachingFramework.Redis.Contracts.RedisObjects;
using CachingFramework.Redis;
using CachingFramework.Redis.Serializers;
using Chat.Api.Entities;

// khong su dung cai nay vi loi redis
namespace Chat.Api.Services
{
    public interface IRedisCacheService
    {
        Task SetMessageGroupAsync(string key, Group data);
        Task<Group?> GetMessageGroupAsync(string key);
        Task AddMessageAsync(string key, Message data);
        Task<List<Message>> GetMessagesAsync(string key, int pageNumber, int pageSize = 10);
    }

    public class RedisCacheService : IRedisCacheService
    {
        private readonly RedisContext redisContext;
        public RedisCacheService(IConfiguration configuration)
        {
            redisContext = new RedisContext(configuration["CacheSettings:ConnectionString"], new JsonSerializer());
        }

        public async Task SetMessageGroupAsync(string key, Group data)
        {
            await redisContext.Cache.SetObjectAsync(key, data, TimeSpan.FromMinutes(15));
        }

        public async Task<Group?> GetMessageGroupAsync(string key)
        {
            try
            {
                var data = await redisContext.Cache.GetObjectAsync<Group>(key);
                return data;
            }
            catch (Exception ex)
            {

                return null;
            }            
        }

        public async Task AddMessageAsync(string key, Message data)
        {
            IRedisList<Message> list = redisContext.Collections.GetRedisList<Message>(key);
            list.TimeToLive = TimeSpan.FromMinutes(15);
            await list.PushFirstAsync(data);
        }

        public Task<List<Message>> GetMessagesAsync(string key, int pageNumber, int pageSize = 10)
        {
            List<Message> messages = new List<Message>();
            try {                
                var list = redisContext.Collections.GetRedisList<Message>(key);
                if (list != null && list.Count > 0)
                {
                    messages = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    messages.Reverse();
                }
            }
            catch
            {

            }
                    
            return Task.FromResult(messages);
        }
    }
}
