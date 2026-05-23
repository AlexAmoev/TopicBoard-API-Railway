using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public interface ITopicService
    {
        Task<List<TopicForGetingDTO>> GetAllTopicsAsync();
        Task<List<TopicWithCommentsGetingDTO>> AllTopicsWithComments();
        Task<List<TopicForGetingDTO>> GetAllTopicsByUserIdAsync(string userId);
        //Task<TopicForGetingDTO> GetSingleTopicsByUserIdAsync(int topicId, string userId);
        Task<List<CommentForGetingDTO>> GetAllCommentsByUserIdAsync(string userId);
        Task<List<CommentForGetingDTO>> GetAllCommentsByTopicIdAsync(int topicId);
        Task<TopicForGetingDTO> GetSingleTopicsAsync(int topicId);
        Task<CommentForGetingDTO> GetSingleCommentByTopicIdAsync(int topicId, int commentId);
        Task<CommentForGetingDTO> GetSingleCommentByUserIdAsync(string userId, int commentId);
        Task AddTopicAsync(TopicForAddingDTO topicForAddingDTO);
        Task AddCommentAsync(CommentForAddingDTO commentForAddingDTO);
        Task UpdateTopicAsync(TopicForUpdatingDTO topicForUpdatingDTO);
        Task UpdateCommentAsync(CommentForUpdatingDTO commentForUpdatingDTO);
        //Task UpdateTopicPartiallyAsync(int topicId, JsonPatchDocument<TopicForUpdatingDTO> patchDocument, ModelStateDictionary modelState);
        Task DeleteTopicAsync(int topicId);
        Task DeleteComment(int commentId);

    }
}
