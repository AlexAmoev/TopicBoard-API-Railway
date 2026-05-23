using IdentityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Entities
{
    public class TopicEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [MaxLength]
        public int CommentsCount { get; set; } = 0;

        //[Required]
        //[MaxLength]
        //public List<Comments>? Comments { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;


        // შეიძლება მთლიანად დავაკომენტარებ
        //[Required]
        //[ForeignKey(nameof(Comments))]
        //public int? CommentId { get; set; }
        //public Comments? Comment { get; set; }


        //ავტორი
        // შეიძლება მომიწევს ამ პარამეტრების დაკომენტირება [Required] / [ForeignKey(nameof(User))]
        //[Required]
        //[ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public State State { get; set; } = State.Pending;


        [Required]
        public Status Status { get; set; } = Status.Active;
    }
}
