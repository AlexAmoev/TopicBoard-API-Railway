using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Entities
{
    public class User : IdentityUser
    {
        //[Required]
        //[ForeignKey(nameof(Comments))]
        //public int? CommentId { get; set; }
        //public Comments? Comment { get; set; }

        //[Required]
        //[ForeignKey(nameof(TopicEntity))]
        //public int? TopicID { get; set; }
        //public TopicEntity? Topic { get; set; }
        public ICollection<TopicEntity>? Topics { get; set; }
        public ICollection<Comments>? Comments { get; set; }
    }
}
