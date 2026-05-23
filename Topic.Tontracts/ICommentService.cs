using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;
using Topic.Models;

namespace Topic.Contracts
{
    public interface ICommentService
    {
        //Task<List<CommentForGetingDTO>> GetAllComments();
        Task<List<CommentForGetingDTO>> GetAllCommentsByTopicIdAsync(int topicId);
        Task<List<CommentForGetingDTO>> GetAllCommentsByUserIdAsync(string userId);
        Task<CommentForGetingDTO> GetSingleCommentByUserIdAsync(string userId, int commentId);
        Task<CommentForGetingDTO> GetSingleCommentByTopicIdAsync(int topicId, int commentId);
        Task AddCommentAsync(CommentForAddingDTO commentForAddingDTO);
        Task UpdateCommentAsync(CommentForUpdatingDTO commentForUpdatingDTO);
        Task DeleteComment(int commentId);
    }
}
