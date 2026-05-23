using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Topic.Contracts;
using Topic.Entities;
using Topic.Models;
using Topic.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Topic.Data;
using Topic.Models.Identity;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

namespace Topic.Service.Implementations
{
    public class TopicServices : ITopicService, ICommentService
    {
        //private readonly string AdminId = "8716071C-1D9B-48FD-B3D0-F059C4FB8031";
        private readonly ITopicRepository _topicRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TopicServices(ITopicRepository topicRepository, IHttpContextAccessor httpContextAccessor,
            ICommentRepository commentRepository, ApplicationDbContext context)
        {
            _topicRepository = topicRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = MappingInitializer.Initialize();
            _commentRepository = commentRepository;
            _context = context;
        }

        public async Task AddCommentAsync(CommentForAddingDTO commentForAddingDTO)
        {
            if (commentForAddingDTO is null)
            {
                throw new ArgumentNullException("Invalid argument passed !");
            }

            var commnet = await _commentRepository.GetSingleCommentAsync(x => x.Id == commentForAddingDTO.TopicEntityId);

            //if (topic.Status == Status.Inactive)
            //{
            //    throw new TopicInactiveException();
            //}

            commentForAddingDTO.AddUserId(AuthenticatedUserId());

            string userId = commentForAddingDTO.GetUserId();

            if (userId.Trim() != AuthenticatedUserId().Trim() && AuthenticatedUserRole().Trim() != "Admin")
                throw new UnauthorizedAccessException("Unauthorized user can't add comment !");

            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            //commentForAddingDTO.GetUserId() = AuthenticatedUserId();

            var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == commentForAddingDTO.TopicEntityId);

            if (topic.Status == Status.Inactive)
            {
                throw new InactiveStatusException();
            }

            var result = _mapper.Map<Comments>(commentForAddingDTO);
            result.User = user;
            result.TopicEntity = topic;
            await _commentRepository.AddCommentAsync(result);
            await _commentRepository.Save();
        }

        public async Task AddTopicAsync(TopicForAddingDTO topicForAddingDTO)
        {

            if (topicForAddingDTO is null)
            {
                throw new ArgumentNullException("Invalid argument passed !");
            }


            //if (topicForAddingDTO.UserId.Trim() != AuthenticatedUserId().Trim() || AuthenticatedUserRole().Trim() != "Admin")
            //    throw new UnauthorizedAccessException("Unauthorized user can't add topic !");

            var user = _context.Users.FirstOrDefault(x => x.Id == AuthenticatedUserId());

            //topicForAddingDTO.addUserId(user.Id);
            //UserDTO dTO = new();
            //dTO.Id = user.Id;
            //dTO.PhoneNumber = user.PhoneNumber;
            //dTO.Email = user.Email;

            //topicForAddingDTO.UserId = AuthenticatedUserId();
            //topicForAddingDTO.User = _mapper.Map<UserDTO>(user);




            var result = _mapper.Map<TopicEntity>(topicForAddingDTO);
            result.UserId = AuthenticatedUserId();

            await _topicRepository.AddTopicAsync(result);
            await _topicRepository.Save();
        }

        public async Task DeleteComment(int commentId)
        {
            if (commentId == 0)
            {
                throw new ArgumentNullException("Invalid argument passed !");
            }

            //var result2 = await _commentRepository.GetSingleCommentAsync(x => x.Id == commentId);

            var result = await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentId);

            var topic = await _topicRepository.GetSingleTopicsAsync(x => x.Id == result.TopicEntityId);

            if (result is null)
            {
                throw new CommentNotFoundException();
            }

            if (result.UserId.Trim() != AuthenticatedUserId().Trim() && AuthenticatedUserRole().Trim() != "Admin")
                throw new UnauthorizedAccessException("Can't delete different users comment !");

            if (topic.Status == Status.Inactive)
            {
                throw new InactiveStatusException();
            }

            

            _commentRepository.DeleteComment(result);
            await _commentRepository.Save();
        }

        public async Task DeleteTopicAsync(int topicId)
        {
            if (topicId <= 0)
            {
                throw new ArgumentException("Invalid argument passed !");
            }

            var result = await _topicRepository.GetSingleTopicsAsync(x => x.Id == topicId);

            if (result == null)
            {
                throw new TopicNotFoundException();
            }

            if (result.UserId.Trim() != AuthenticatedUserId().Trim() && AuthenticatedUserRole().Trim() != "Admin")
                throw new UnauthorizedAccessException("Can't delete different users  topic !");

            _topicRepository.DeleteTopic(result);
            await _topicRepository.Save();
        }

        public async Task<List<CommentForGetingDTO>> GetAllCommentsByTopicIdAsync(int topicId)
        {
            List<CommentForGetingDTO> result = new();
            if (topicId == 0)
            {
                throw new ArgumentNullException("Invalid argument passed !");
            }

            var raw = await _commentRepository.GetAllComments(x => x.TopicEntityId == topicId);

            if (raw is null)
            {
                throw new CommentNotFoundException();
            }

            result = _mapper.Map<List<CommentForGetingDTO>>(raw);

            foreach (var topic in result)
            {
                UserDTO user = _context.Users
                    .Where(x => x.Id == topic.UserId)
                    .Select(x => new UserDTO
                    {
                        Id = x.Id,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber
                    })
                    .FirstOrDefault();

                topic.User = user;
            }

            return result;
        }

        public async Task<List<CommentForGetingDTO>> GetAllCommentsByUserIdAsync(string userId)
        {
            List<CommentForGetingDTO> result = new();

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("Invalid argument passed !");
            }

            var raw = await _commentRepository.GetAllComments(x => x.UserId == userId);

            if (raw is null)
            {
                throw new CommentNotFoundException();
            }

            result = _mapper.Map<List<CommentForGetingDTO>>(raw);

            foreach (var topic in result)
            {
                UserDTO user = _context.Users
                    .Where(x => x.Id == topic.UserId)
                    .Select(x => new UserDTO
                    {
                        Id = x.Id,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber
                    })
                    .FirstOrDefault();

                topic.User = user;
            }

            return result;
        }

        public async Task<List<TopicForGetingDTO>> GetAllTopicsAsync()
        {
            var raw = await _topicRepository.GetAllTopicsAsync();

            if (raw.Count == 0)
            {
                throw new TopicNotFoundException();
            }

            List<TopicEntity> topics = new();

            //if (AuthenticatedUserRole() != "Admin")
            if (AuthenticatedUserRole() != "Admin" && AuthenticatedUserRole() == "Customer")
            {
                foreach (var topic in raw)
                {
                    if (AuthenticatedUserId() == topic.UserId /* && topic.State != State.Hide && topic.State == State.Hide*/ )
                    {
                        topics.Add(topic);
                    }
                    else if (AuthenticatedUserId() != topic.UserId && topic.State != State.Hide)
                    {
                        topics.Add(topic);
                    }
                }
            }
            else if (AuthenticatedUserRole() != "Admin" && AuthenticatedUserRole() != "Customer")
            {
                foreach (var topic in raw)
                {
                    if (topic.State != State.Hide)
                    {
                        topics.Add(topic);
                    }
                }
            }
            else
            {
                foreach (var topic in raw)
                {
                    topics.Add(topic);
                }
            }

            for (int i = 0; i < topics.Count; i++)
            {
                int count = 0;
                TopicEntity topic = topics[i];
                List<Comments> comments = await _commentRepository.GetAllComments(x => x.TopicEntityId == topic.Id);

                for (int j = 0; j < comments.Count; j++)
                {
                    count++;
                }
                topic.CommentsCount = count;
            }

            List<TopicForGetingDTO> result = _mapper.Map<List<TopicForGetingDTO>>(topics);


            foreach (var topic in result)
            {
                UserDTO user = _context.Users
                    .Where(u => u.Id == topic.UserId)
                    .Select(u => new UserDTO
                    {
                        Id = u.Id,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber
                    })
                    .FirstOrDefault();

                topic.User = user;
            }

            return result;
        }

        public async Task<List<TopicWithCommentsGetingDTO>> AllTopicsWithComments()
        {
            var raw = await _topicRepository.GetAllTopicsAsync();

            if (raw.Count == 0)
            {
                throw new TopicNotFoundException();
            }

            List<TopicEntity> topics = new();

            //if (AuthenticatedUserRole() != "Admin")
            if (AuthenticatedUserRole() != "Admin" && AuthenticatedUserRole() == "Customer")
            {
                foreach (var topic in raw)
                {
                    if (AuthenticatedUserId() == topic.UserId /* && topic.State != State.Hide && topic.State == State.Hide*/ )
                    {
                        topics.Add(topic);
                    }
                    else if (AuthenticatedUserId() != topic.UserId && topic.State != State.Hide)
                    {
                        topics.Add(topic);
                    }
                }
            }
            else if (AuthenticatedUserRole() != "Admin" && AuthenticatedUserRole() != "Customer")
            {
                foreach (var topic in raw)
                {
                    if (topic.State != State.Hide)
                    {
                        topics.Add(topic);
                    }
                }
            }
            else
            {
                foreach (var topic in raw)
                {
                    topics.Add(topic);
                }
            }

            for (int i = 0; i < topics.Count; i++)
            {
                int count = 0;
                TopicEntity topic = topics[i];
                List<Comments> comments = await _commentRepository.GetAllComments(x => x.TopicEntityId == topic.Id);

                for (int j = 0; j < comments.Count; j++)
                {
                    count++;
                }
                topic.CommentsCount = count;
            }

            List<TopicWithCommentsGetingDTO> result = _mapper.Map<List<TopicWithCommentsGetingDTO>>(topics);


            foreach (var topic in result)
            {
                UserDTO user = _context.Users
                    .Where(x => x.Id == topic.UserId)
                    .Select(x => new UserDTO
                    {
                        Id = x.Id,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber
                    })
                    .FirstOrDefault();

                topic.User = user;
            }

            for (int k = 0; k < result.Count; k++)
            {

                result[k].comment = new CommentWithTopicDTO[result[k].CommentsCount];
                //for (int l = 0; l < result[k].CommentsCount; l++)
                //{
                //    result[k].comment = new CommentWithTopicDTO[result[l].CommentsCount];
                //}
            }
            for (int l = 0; l < result.Count; l++)
            {
                for (int t = 0; t < result[l].CommentsCount; t++)
                {
                    result[l].comment[t] = new CommentWithTopicDTO { Id = 0, UserName = "", Comment = "", PostedDate = DateTime.Now, TopicEntityId = 0 };
                    //result[l].comment[t]
                }
            }

            //for (int i = 0; i < result.Count; i++)
            //{
            //    for (int j = 0; j < result[i].CommentsCount; j++)
            //    {
            //        for (int k = 0; k < result[j].comment.Length; k++)
            //        {
            //            result[i].comment[j] = await _commentRepository.GetAllComments(x => x.Id == result[i].Id )
            //        }
            //    }
            //}

            for (int i = 0; i < result.Count; i++)
            {
                //result[i].commentId = comment.Id;
                for (int j = 0; j < result[i].CommentsCount; j++)
                {
                    List<Comments> rawCom = await _commentRepository.GetAllComments(x => x.TopicEntityId == result[i].Id);

                    List<CommentWithTopicDTO> commentWithTopicDTOs = _mapper.Map<List<CommentWithTopicDTO>>(rawCom);

                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == rawCom[j].UserId);

                    result[i].comment[j].Id = commentWithTopicDTOs[j].Id;
                    result[i].comment[j].UserName = user.UserName; //rawCom[j].User.UserName;  //commentWithTopicDTOs[j].UserName;
                    result[i].comment[j].Comment = commentWithTopicDTOs[j].Comment;
                    result[i].comment[j].PostedDate = commentWithTopicDTOs[j].PostedDate;
                    result[i].comment[j].TopicEntityId = commentWithTopicDTOs[j].TopicEntityId;
                    //CommentWithTopicDTO comment = await _context.Comments
                    //    .Where(x => x.TopicEntityId == result[i].Id /* && x.Id == result[i].comment[i].Id *//* && x.UserId == result[j].UserId */)
                    //    .Select(x => new CommentWithTopicDTO
                    //    {
                    //        Id = x.Id,
                    //        UserName = x.User.UserName,
                    //        Comment = x.Comment,
                    //        PostedDate = x.PostedDate,
                    //        TopicEntityId = result[i].Id,
                    //        //CommentId = result[i].
                    //    })
                    //    .FirstOrDefaultAsync();

                    //result[i].comment[j].Id = comment.Id;
                    //result[i].comment[j].UserName = comment.UserName;
                    //result[i].comment[j].Comment = comment.Comment;
                    //result[i].comment[j].PostedDate = comment.PostedDate;
                    //result[i].comment[j].TopicEntityId = comment.TopicEntityId;


                    //for (int k = 0; k < result[j].comment.Length; k++)
                    //{
                    //    CommentWithTopicDTO comment = await _context.Comments
                    //    //CommentWithTopicDTO comment = await _commentRepository.GetAllComments(x => x.TopicEntityId == result[i].Id && x.Id == result[i].comment[k].Id)
                    //    .Where(x => x.TopicEntityId == result[i].Id /* && x.Id == result[i].comment[k].Id */ && x.UserId == result[i].UserId)
                    //    .Select(x => new CommentWithTopicDTO
                    //    {
                    //        Id = x.Id,
                    //        UserName = x.User.UserName,
                    //        Comment = x.Comment,
                    //        PostedDate = x.PostedDate,
                    //        TopicEntityId = result[i].Id,
                    //        //CommentId = result[i].
                    //    })
                    //    .FirstOrDefaultAsync();

                    //    result[i].comment[j].Id = comment.Id;
                    //    result[i].comment[j].UserName = comment.UserName;
                    //    result[i].comment[j].Comment = comment.Comment;
                    //    result[i].comment[j].PostedDate = comment.PostedDate;
                    //    result[i].comment[j].TopicEntityId = comment.TopicEntityId;
                    //}








                    //result[i].comment.Add(comment);
                }

                //for (int j = 0; j < result[i].comment.Count; j++)
                //{
                //    if (result[i].commentId == comment.Id)
                //    {
                //        result[i].comment.Add(comment);
                //    }
                //}

                //result[i].comment.Add(comment);

            }



            return result;
        }
        public async Task<List<TopicForGetingDTO>> GetAllTopicsByUserIdAsync(string userId)
        {
            List<TopicForGetingDTO> result = new();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("Invalid argument passed !");
            }

            var raw = await _topicRepository.GetAllTopicsAsync(x => x.UserId.Trim() == userId.Trim());

            if (raw is null)
            {
                throw new TopicNotFoundException();
            }

            List<TopicEntity> topics = new();

            //if (AuthenticatedUserRole() != "Admin")
            if (AuthenticatedUserRole() != "Admin" && AuthenticatedUserRole() == "Customer")
            {
                foreach (var topic in raw)
                {
                    if (AuthenticatedUserId() != topic.UserId && topic.State != State.Hide)
                    {
                        topics.Add(topic);
                    }
                    else
                    {
                        topics.Add(topic);
                    }
                }
            }
            else if (AuthenticatedUserRole() != "Admin" && AuthenticatedUserRole() != "Customer")
            {
                foreach (var topic in raw)
                {
                    if (topic.State != State.Hide)
                    {
                        topics.Add(topic);
                    }
                }
            }
            else
            {
                foreach (var topic in raw)
                {
                    topics.Add(topic);
                }
            }

            for (int i = 0; i < topics.Count; i++)
            {
                int count = 0;
                TopicEntity topic = topics[i];
                List<Comments> comments = await _commentRepository.GetAllComments(x => x.TopicEntityId == topic.Id);

                for (int j = 0; j < comments.Count; j++)
                {
                    count++;
                }
                topic.CommentsCount = count;
            }

            result = _mapper.Map<List<TopicForGetingDTO>>(topics);

            foreach (var topic in result)
            {
                UserDTO user = _context.Users
                    .Where(x => x.Id == topic.UserId)
                    .Select(x => new UserDTO
                    {
                        Id = x.Id,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber
                    })
                    .FirstOrDefault();

                topic.User = user;
            }

            return result;
        }

        public async Task<CommentForGetingDTO> GetSingleCommentByTopicIdAsync(int topicId, int commentId)
        {
            if (commentId == 0 || topicId == 0)
            {
                throw new ArgumentNullException();
            }

            var raw = await _commentRepository.GetSingleCommentAsync(x => x.Id == commentId && x.TopicEntityId == topicId);

            if (raw is null)
            {
                throw new CommentNotFoundException();
            }

            var result = _mapper.Map<CommentForGetingDTO>(raw);

            UserDTO user = _context.Users
                .Where(x => x.Id == result.UserId)
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber
                })
                .FirstOrDefault();
            result.User = user;

            return result;
        }

        public async Task<CommentForGetingDTO> GetSingleCommentByUserIdAsync(string userId, int commentId)
        {
            if (commentId == 0 || userId is null)
            {
                throw new ArgumentNullException();
            }

            var raw = await _commentRepository.GetSingleCommentAsync(x => x.Id == commentId && x.UserId == userId);

            if (raw is null)
            {
                throw new CommentNotFoundException();
            }

            var result = _mapper.Map<CommentForGetingDTO>(raw);

            UserDTO user = _context.Users
                .Where(x => x.Id == result.UserId)
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber
                })
                .FirstOrDefault();

            result.User = user;

            return result;
        }

        public async Task<TopicForGetingDTO> GetSingleTopicsAsync(int topicId)
        {
            if (topicId == 0)
            {
                throw new ArgumentException("Invalid argument passed !");
            }

            var raw = await _topicRepository.GetSingleTopicsAsync(x => x.Id == topicId);

            if (raw is null)
            {
                throw new TopicNotFoundException();
            }

            TopicEntity topics = new();

            if (AuthenticatedUserRole() != "Admin" || AuthenticatedUserId() != raw.UserId)
            {
                if (raw.State == State.Hide)
                {
                    throw new TopicIsHidenException();
                }
            }
            else
            {
                topics = raw;
            }

            int count = 0;
            List<Comments> comments = await _commentRepository.GetAllComments(x => x.TopicEntityId == topics.Id);
            for (int i = 0; i < comments.Count; i++)
            {
                count++;
            }
            topics.CommentsCount = count;

            var result = _mapper.Map<TopicForGetingDTO>(topics);

            UserDTO user = _context.Users
                    .Where(u => u.Id == result.UserId)
                    .Select(u => new UserDTO
                    {
                        Id = u.Id,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber
                    })
                    .FirstOrDefault();

            result.User = user;

            return result;
        }

        //public async Task<TopicForGetingDTO> GetSingleTopicsByUserIdAsync(int topicId, string userId)
        //{
        //    if (topicId == 0 || userId is null)
        //    {
        //        throw new ArgumentException("Invalid argument passed !");
        //    }

        //    var raw = await _topicRepository.GetSingleTopicsAsync(x => x.Id == topicId && x.UserId == userId);

        //    if (raw is null)
        //    {
        //        throw new TopicNotFoundException();
        //    }

        //    var result = _mapper.Map<TopicForGetingDTO>(raw);
        //    return result;
        //}

        public async Task UpdateCommentAsync(CommentForUpdatingDTO commentForUpdatingDTO)
        {
            if (commentForUpdatingDTO is null)
            {
                throw new ArgumentNullException("Invalid argument passed !");
            }

            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentForUpdatingDTO.Id);

            var userId = comment.UserId;

            commentForUpdatingDTO.addUserId(userId);

            if (userId.Trim() != AuthenticatedUserId().Trim() && AuthenticatedUserRole().Trim() != "Admin")
                throw new UnauthorizedAccessException("Unauthorized user can't update comment !");

            var topic = await _topicRepository.GetSingleTopicsAsync(x => x.Id == commentForUpdatingDTO.TopicEntityId);


            if (AuthenticatedUserRole() != "Admin")
            {
                if (topic.Status == Status.Inactive)
                {
                    throw new InactiveStatusException();
                }
            }
            



            var result = _mapper.Map<Comments>(commentForUpdatingDTO);
            await _commentRepository.UpdateCommentAsync(result);
            await _commentRepository.Save();
        }

        public async Task UpdateTopicAsync(TopicForUpdatingDTO topicForUpdatingDTO)
        {
            if (topicForUpdatingDTO is null)
            {
                throw new ArgumentNullException("Invalid agument passed !");
            }

            var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == topicForUpdatingDTO.Id);

            var user = _context.Users.FirstOrDefault(x => x.Id == topic.UserId);

            if (user.Id != AuthenticatedUserId().Trim() && AuthenticatedUserRole().Trim() != "Admin")
            {
                throw new UnauthorizedAccessException("Unauthorized user can't update topic !");
            }

            if (AuthenticatedUserRole() != "Admin")
            {
                if (topic.Status == Status.Inactive)
                {
                    throw new InactiveStatusException();
                }
            }
            

            topicForUpdatingDTO.addUserId(AuthenticatedUserId());

            var result = _mapper.Map<TopicEntity>(topicForUpdatingDTO);
            result.User = user;
            result.UserId = user.Id;
            await _topicRepository.UpdateTopicAsync(result);
            await _topicRepository.Save();
        }

        //public async Task UpdateTopicPartiallyAsync(int topicId, JsonPatchDocument<TopicForUpdatingDTO> patchDocument, ModelStateDictionary modelState)
        //{
        //    if (topicId <= 0)
        //        throw new ArgumentException("Invalid argument passed");

        //    TopicEntity rawTopic = await _topicRepository.GetSingleTopicsAsync(x => x.Id == topicId);

        //    if (rawTopic == null)
        //        throw new TopicNotFoundException();

        //    TopicForUpdatingDTO topicToPatch = _mapper.Map<TopicForUpdatingDTO>(rawTopic);
        //    patchDocument.ApplyTo(topicToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)modelState);

        //    if (!modelState.IsValid)
        //        throw new InvalidDataException("Invalid paraemeters passed operation type, updateable property path ro value is incorrect !");

        //    _mapper.Map(topicToPatch, rawTopic);

        //    await _topicRepository.Save();
        //}

        private string AuthenticatedUserId()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return result;
            }
            else
            {
                throw new UnauthorizedAccessException("Can't get credentials of unauthorized user");
            }
        }

        private string AuthenticatedUserRole()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

                return result;
            }
            else if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated == false)
            {
                return "unauthorized";
            }
            else
            {
                throw new UnauthorizedAccessException("Can't get credentials of unauthorized user");
            }
        }
    }
}
