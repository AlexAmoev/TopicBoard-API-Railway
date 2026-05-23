using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Topic.Contracts;
using Topic.Data;
using Topic.Entities;

namespace Topic.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCommentAsync(Comments commentEntity)
        {
            if (commentEntity != null)
            {
                await _context.Comments.AddAsync(commentEntity);
            }
        }

        public void DeleteComment(Comments commentEntity)
        {
            if (commentEntity != null)
            {
                _context.Comments.Remove(commentEntity);
            }
        }

        public async Task<List<Comments>> GetAllComments()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<List<Comments>> GetAllComments(Expression<Func<Comments, bool>> filter)
        {
            return await _context.Comments
                .Where(filter)
                .ToListAsync();
        }

        //public async Task<List<Comments>> GetAllCommentsByTopicIdAsync(Expression<Func<Comments, bool>> filter)
        //{
        //    return await _context.Comments
        //        .Where(filter)
        //        .ToListAsync();
        //}

        //public async Task<List<Comments>> GetAllCommentsByUserIdAsync(Expression<Func<Comments, bool>> filter)
        //{
        //    return await _context.Comments
        //        .Where(filter)
        //        .ToListAsync();
        //}

        public async Task<Comments> GetSingleCommentAsync(Expression<Func<Comments, bool>> filter)
        {
            return await _context.Comments
                //.Where(filter)
                .FirstOrDefaultAsync(filter);
        }

        public async Task UpdateCommentAsync(Comments commentEntity)
        {
            if (_context.Comments != null)
            {
                var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentEntity.Id);
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == commentToUpdate.UserId);
                var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == commentEntity.TopicEntityId);


                if (commentToUpdate != null)
                {
                    commentToUpdate.Id = commentEntity.Id;
                    commentToUpdate.Comment = commentEntity.Comment;
                    commentToUpdate.PostedDate = commentEntity.PostedDate;
                    commentToUpdate.TopicEntityId = commentEntity.TopicEntityId;
                    commentToUpdate.TopicEntity = topic;
                    commentToUpdate.UserId = user.Id;
                    commentToUpdate.User = user;

                    _context.Comments.Update(commentToUpdate);
                }
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
