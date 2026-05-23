//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Topic.Contracts;
//using Topic.Entities;
//using Topic.Models;
//using Topic.Service.Exceptions;

//namespace Topic.Service.Implementations
//{
//    public class CommentServices : ICommentService
//    {
//        //private readonly string AdminId = "8716071C-1D9B-48FD-B3D0-F059C4FB8031";
//        private readonly ICommentRepository _commentRepository;
//        private readonly IMapper _mapper;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        public CommentServices(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor)
//        {
//            _commentRepository = commentRepository;
//            _mapper = MappingInitializer.Initialize();
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task AddCommentAsync(CommentForAddingDTO commentForAddingDTO)
//        {
//            if (commentForAddingDTO is null)
//            {
//                throw new ArgumentNullException("Invalid argument passed !");
//            }
//            string userId = commentForAddingDTO.GetUserId();

//            if (userId.Trim() != AuthenticatedUserId().Trim() || AuthenticatedUserRole().Trim() != "Admin")
//                throw new UnauthorizedAccessException("Unauthorized user can't add comment !");

//            var result = _mapper.Map<Comments>(commentForAddingDTO);
//            await _commentRepository.AddCommentAsync(result);
//            await _commentRepository.Save();
//        }

//        public async Task DeleteComment(int commentId)
//        {
//            if (commentId == 0)
//            {
//                throw new ArgumentNullException("Invalid argument passed !");
//            }

//            var result = await _commentRepository.GetSingleCommentAsync(x => x.Id == commentId);

//            if (result is null)
//            {
//                throw new CommentNotFoundException();
//            }

//            if (result.UserId.Trim() != AuthenticatedUserId().Trim() || AuthenticatedUserRole().Trim() != "Admin")
//                throw new UnauthorizedAccessException("Can't delete different users comment !");

//            _commentRepository.DeleteComment(result);
//            await _commentRepository.Save();
//        }

//        public async Task<List<CommentForGetingDTO>> GetAllCommentsByTopicIdAsync(int topicId)
//        {
//            List<CommentForGetingDTO> result = new();
//            if (topicId == 0)
//            {
//                throw new ArgumentNullException("Invalid argument passed !");
//            }

//            var raw = await _commentRepository.GetAllComments(x => x.TopicEntityId == topicId);

//            if (raw is null)
//            {
//                throw new CommentNotFoundException();
//            }

//            result = _mapper.Map<List<CommentForGetingDTO>>(raw);
//            return result;
//        }

//        public async Task<List<CommentForGetingDTO>> GetAllCommentsByUserIdAsync(string userId)
//        {
//            List<CommentForGetingDTO> result = new();

//            if (string.IsNullOrWhiteSpace(userId))
//            {
//                throw new ArgumentNullException("Invalid argument passed !");
//            }

//            var raw = await _commentRepository.GetAllComments(x =>x.UserId == userId);

//            if (raw is null)
//            {
//                throw new CommentNotFoundException();
//            }

//            result = _mapper.Map<List<CommentForGetingDTO>>(raw);
//            return result;
//        }

//        public async Task<CommentForGetingDTO> GetSingleCommentByUserIdAsync(string userId, int commentId)
//        {
//            if (commentId == 0 || userId is null)
//            {
//                throw new ArgumentNullException();
//            }

//            var raw = await _commentRepository.GetSingleCommentAsync(x => x.Id == commentId && x.UserId == userId);

//            if (raw is null)
//            {
//                throw new CommentNotFoundException();
//            }

//            var result = _mapper.Map<CommentForGetingDTO>(raw);

//            return result;
//        }

//        public async Task<CommentForGetingDTO> GetSingleCommentByTopicIdAsync(int topicId, int commentId)
//        {
//            if (commentId == 0 || topicId == 0)
//            {
//                throw new ArgumentNullException();
//            }

//            var raw = await _commentRepository.GetSingleCommentAsync(x => x.Id == commentId && x.TopicEntityId == topicId);

//            if (raw is null)
//            {
//                throw new CommentNotFoundException();
//            }

//            var result = _mapper.Map<CommentForGetingDTO>(raw);

//            return result;
//        }
//        public async Task UpdateCommentAsync(CommentForUpdatingDTO commentForUpdatingDTO)
//        {
//            if (commentForUpdatingDTO is null)
//            {
//                throw new ArgumentNullException("Invalid argument passed !");
//            }

//            if (commentForUpdatingDTO.UserId.Trim() != AuthenticatedUserId().Trim() || AuthenticatedUserRole().Trim() != "Admin")
//                throw new UnauthorizedAccessException("Unauthorized user can't update comment !");

//            var result = _mapper.Map<Comments>(commentForUpdatingDTO);
//            await _commentRepository.UpdateCommentAsync(result);
//            await _commentRepository.Save();
//        }

//        private string AuthenticatedUserId()
//        {
//            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
//            {
//                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
//                return result;
//            }
//            else
//            {
//                throw new UnauthorizedAccessException("Can't get credentials of unauthorized user");
//            }
//        }

//        private string AuthenticatedUserRole()
//        {
//            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
//            {
//                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
//                return result;
//            }
//            else
//            {
//                throw new UnauthorizedAccessException("Can't get credentials of unauthorized user");
//            }
//        }
//    }
//}
