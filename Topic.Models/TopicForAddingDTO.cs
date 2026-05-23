using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;
using System.Text.Json.Serialization;
using Topic.Data;
using Topic.Models.Identity;

namespace Topic.Models
{
    public class TopicForAddingDTO
    {

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        //[JsonIgnore]
        //public int CommentsCount { get; set; } = 0;

        //[Required]
        //[JsonIgnore]
        //[MaxLength]
        //public List<Comments>? Comments { get; set; }

        //[JsonIgnore]
        //public DateTime StartDate { get; set; } = DateTime.Now;

        //[Required]
        //[ForeignKey(nameof(Comments))]
        //public int? CommentId { get; set; }
        //public Comments? comment { get; set; }

        //ავტორი
        //[Required]
        //[ForeignKey(nameof(User))]
        private string? UserId { get; set; }
        //public UserDTO? User { get; set; }

        //[Required]
        //public State State { get; set; } = State.Pending;


        //[Required]
        //public Status Status { get; set; } = Status.Active;


        //public TopicForAddingDTO AddUserId(string userId)
        //{ 
        //    this.UserId = userId;
        //    return this;
        //}

        public void addUserId(string userId)
        { this.UserId = userId; }
    }
}
