using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;

namespace Topic.Contracts
{
    public interface ICommentRepository : ISaveble
    {
        Task<List<Comments>> GetAllComments();
        Task<List<Comments>> GetAllComments(Expression<Func<Comments, bool>> filter);
        Task<Comments> GetSingleCommentAsync(Expression<Func<Comments, bool>> filter);
        Task AddCommentAsync(Comments commentEntity);
        Task UpdateCommentAsync(Comments commentEntity);
        void DeleteComment(Comments commentEntity);
    }
}
