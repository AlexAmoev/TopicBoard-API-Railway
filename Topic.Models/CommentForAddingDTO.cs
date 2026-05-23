using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;

namespace Topic.Models
{
    public class CommentForAddingDTO
    {

        [Required]
        public int? TopicEntityId { get; set; }

        [Required]
        [MaxLength]
        public string? Comment { get; set; }

        //[Required]
        //public DateTime PostedDate { get; set; } = DateTime.Now;

        //[Required]
        //[ForeignKey(nameof(TopicEntity))]
        //public TopicEntity? TopicEntity { get; set; }

        //[Required]
        //[ForeignKey(nameof(User))]
        //[Required]
        private string? UserId { get; set; }
        //public User? User { get; set; }
        public void AddUserId(string userId)
        {
            this.UserId = userId;
        }
        public string? GetUserId()
        {
            return UserId;
        }
    }
}
