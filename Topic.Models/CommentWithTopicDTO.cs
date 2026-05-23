using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;

namespace Topic.Models
{
    public class CommentWithTopicDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Comment { get; set; }
        public DateTime PostedDate { get; set; }
        public int? TopicEntityId { get; set; } // need to be hiden
        //public int? CommentId { get; set; }
    }
}
