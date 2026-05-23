using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Topic.Data;
using Topic.Entities;
using Topic.Contracts;
using Microsoft.EntityFrameworkCore;


namespace Topic.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ApplicationDbContext _context;

        public TopicRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddTopicAsync(TopicEntity topicEntity)
        {
            if (topicEntity != null)
            {
                await _context.Topics.AddAsync(topicEntity);
            }
        }

        public void DeleteTopic(TopicEntity topicEntity)
        {
            if (topicEntity != null)
            {
                _context.Topics.Remove(topicEntity);
            }
        }

        public async Task<List<TopicEntity>> GetAllTopicsAsync()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<List<TopicEntity>> GetAllTopicsAsync(Expression<Func<TopicEntity, bool>> filter)
        {
            return await _context.Topics
                .Where(filter)
                .ToListAsync();
        }

        //public async Task<List<TopicEntity>> GetAllTopicsByUserIdAsync(Expression<Func<TopicEntity, bool>> filter)
        //{
        //    return await _context.Topics
        //        .Where(filter)
        //        .ToListAsync();
        //}

        public async Task<TopicEntity> GetSingleTopicsAsync(Expression<Func<TopicEntity, bool>> filter)
        {
            return await _context.Topics
                .Where(filter)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateTopicAsync(TopicEntity topicEntity)
        {
            if (topicEntity != null)
            {
                var topicToUpdate = await _context.Topics.FirstOrDefaultAsync(x => x.Id == topicEntity.Id);

                if (topicToUpdate != null)
                {
                    topicToUpdate.Title = topicEntity.Title;
                    topicToUpdate.CommentsCount = topicEntity.CommentsCount;
                    topicToUpdate.StartDate = topicEntity.StartDate;
                    //topicToUpdate.CommentId = topicEntity.CommentId;
                    topicToUpdate.UserId = topicEntity.UserId;
                    topicToUpdate.State = topicEntity.State;
                    topicToUpdate.Status = topicEntity.Status;

                    _context.Topics.Update(topicToUpdate);
                }
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
