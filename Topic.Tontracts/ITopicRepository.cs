using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;

namespace Topic.Contracts
{
    public interface ITopicRepository : ISaveble
    {
        Task<List<TopicEntity>> GetAllTopicsAsync();
        Task<List<TopicEntity>> GetAllTopicsAsync(Expression<Func<TopicEntity, bool>> filter);
        //Task<List<TopicEntity>> GetAllTopicsByUserIdAsync(Expression<Func<TopicEntity, bool>> filter);
        Task<TopicEntity> GetSingleTopicsAsync(Expression<Func<TopicEntity, bool>> filter);
        Task AddTopicAsync(TopicEntity topicEntity);
        Task UpdateTopicAsync(TopicEntity topicEntity);
        void DeleteTopic(TopicEntity topicEntity);
        //Task DeleteTopic(Task<TopicEntity> result);
    }
}
