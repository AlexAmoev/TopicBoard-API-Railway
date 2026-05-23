using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Topic.Contracts;
using Topic.Models;
using Topic.Service.Implementations;

namespace Topic.API.Controllers
{
    [Route("api/topics")]
    [ApiController]
    //[Authorize] // დიდი ალბათობით კონკრეტულ მეთოდებს მივანიჭებ
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly ICommentService _commentService;
        private ApiResponse _response;

        public TopicController(ITopicService topicService, ICommentService commentService)
        {
            _topicService = topicService;
            _commentService = commentService;
            _response = new();
        }

        [HttpGet("AllTopics")]
        public async Task<IActionResult> AllTopics()
        {
            var result = await _topicService.GetAllTopicsAsync();

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpGet("AllTopicsWithComments")]
        public async Task<IActionResult> AllTopicsWithComments()
        {
            var result = await _topicService.AllTopicsWithComments();

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpGet("AllTopicsByUser{userId:guid}")]
        public async Task<IActionResult> AllTopicsByUser([FromRoute] string userId)
        {
            var result = await _topicService.GetAllTopicsByUserIdAsync(userId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpGet("AllCommentsByUserId{userId:guid}")]
        public async Task<IActionResult> AllCommentsByUserId([FromRoute] string userId)
        {
            var result = await _topicService.GetAllCommentsByUserIdAsync(userId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpGet("AllCommentsByTopicId{topicId:int}")]
        public async Task<IActionResult> AllCommentsByTopicId([FromRoute] int topicId)
        {
            var result = await _topicService.GetAllCommentsByTopicIdAsync(topicId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        //[HttpGet("SingleTopicOfUser{userId:guid}/{topicId:int}")]
        //public async Task<IActionResult> SingleTopicOfUser([FromRoute] string userId, [FromRoute] int topicId)
        //{
        //    var result = await _topicService.GetSingleTopicsByUserIdAsync(topicId, userId);

        //    _response.Result = result;
        //    _response.IsSuccess = true;
        //    _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
        //    _response.Message = "Request completed successfully";

        //    return StatusCode(_response.StatusCode, _response);
        //}

        [HttpGet("SingleCommentByUser{userId:guid}/{commentId:int}")]
        public async Task<IActionResult> SingleCommentByUser([FromRoute] string userId, [FromRoute] int commentId)
        {
            var result = await _topicService.GetSingleCommentByUserIdAsync(userId, commentId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpGet("SingleCommentByTopic{topicId:int}/{commentId:int}")]
        public async Task<IActionResult> SingleCommentByTopic([FromRoute] int topicId, [FromRoute] int commentId)
        {
            var result = await _topicService.GetSingleCommentByTopicIdAsync(topicId, commentId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [Authorize]
        [HttpPost("AddTopic")]
        public async Task<IActionResult> AddTopic([FromForm] TopicForAddingDTO model)
        {

            await _topicService.AddTopicAsync(model);

            _response.Result = model;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.Created);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [Authorize]
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] CommentForAddingDTO comment) // [FromBody]-ის რო ვანიჭებ არ იღებს მონაცემებს
        {
            await _topicService.AddCommentAsync(comment);

            _response.Result = comment;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.Created);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [Authorize]
        [HttpPut("UpdateTopic")]
        public async Task<IActionResult> UpdateTopic([FromForm] TopicForUpdatingDTO model)
        {
            await _topicService.UpdateTopicAsync(model);

            _response.Result = model;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [Authorize]
        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment([FromForm] CommentForUpdatingDTO model)
        {
            await _topicService.UpdateCommentAsync(model);

            _response.Result = model;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        //[HttpPatch("{topicId:int}")]
        //public async Task<IActionResult> UpdateTopicPartiallyAsync([FromRoute] int topicId, [FromForm] JsonPatchDocument<TopicForUpdatingDTO> patchDocument)
        //{
        //    await _topicService.UpdateTopicPartiallyAsync(topicId, patchDocument, ModelState);

        //    _response.Result = patchDocument;
        //    _response.IsSuccess = true;
        //    _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
        //    _response.Message = "Request completed successfully";

        //    return StatusCode(_response.StatusCode, _response);
        //}

        [Authorize]
        [HttpDelete("DeleteTopic{topicId:int}")]
        public async Task<IActionResult> DeleteTopic([FromRoute] int topicId)
        {
            await _topicService.DeleteTopicAsync(topicId);

            _response.Result = topicId;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.NoContent);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [Authorize]
        [HttpDelete("DeleteComment{commentId:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
        {
            await _topicService.DeleteComment(commentId);

            _response.Result = commentId;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.NoContent);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }
    }
}
