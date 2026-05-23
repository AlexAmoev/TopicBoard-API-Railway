using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;
using Topic.Models.Identity;

namespace Topic.Models
{
    public class CommentForGetingDTO
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public DateTime PostedDate { get; set; }
        public int? TopicEntityId { get; set; }
        //public TopicEntity? TopicEntity { get; set; }
        public string? UserId { get; set; }
        public UserDTO? User { get; set; }
    }
}
