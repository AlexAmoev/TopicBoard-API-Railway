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
    public class CommentForUpdatingDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int? TopicEntityId { get; set; }

        [MaxLength]
        public string? Comment { get; set; }

        [Required]
        public DateTime PostedDate { get; set; } = DateTime.Now;

        //[Required]
        //[ForeignKey(nameof(TopicEntity))]
        //public int? TopicEntityId { get; set; }
        //public TopicEntity? TopicEntity { get; set; }

        //[Required]
        //[ForeignKey(nameof(User))]
        //public string? UserId { get; set; }
        //public User? User { get; set; }

        //[Required]
        private string? UserId { get; set; }

        public void addUserId(string userId)
        { this.UserId = userId; }
    }
}
