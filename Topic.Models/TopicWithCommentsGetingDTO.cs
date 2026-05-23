using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;
using Topic.Models.Identity;

namespace Topic.Models
{
    public class TopicWithCommentsGetingDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int CommentsCount { get; set; }
        public DateTime StartDate { get; set; }
        //public int? CommentId { get; set; }
        public string? UserId { get; set; }
        //public User? User { get; set; }
        public State State { get; set; }
        public Status Status { get; set; }
        public UserDTO? User { get; set; }
        //public int commentId { get; set; }
        public CommentWithTopicDTO[]? comment { get; set; }
        //public ICollection<CommentWithTopicDTO>? comment { get; set; }
    }
}
