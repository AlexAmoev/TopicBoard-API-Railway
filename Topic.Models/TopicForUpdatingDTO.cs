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
    public class TopicForUpdatingDTO
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        //[Required]
        //public int CommentsCount { get; set; } = 0;

        //[Required]
        //[MaxLength]
        //public List<Comments>? Comments { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        //[Required]
        //[ForeignKey(nameof(Comments))]
        //public int? CommentId { get; set; }
        //public Comments? comment { get; set; }


        //ავტორი
        //[Required]
        //[ForeignKey(nameof(User))]
        //public string? UserId { get; set; }
        //public User? User { get; set; }
        
        //[Required]
        private string? UserId { get; set; }

        [Required]
        public State State { get; set; } = State.Pending;


        [Required]
        public Status Status { get; set; } = Status.Active;

        public void addUserId(string userId)
        { this.UserId = userId; }
    }
}
