using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Entities
{
    public class Comments
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength]
        public string? Comment { get; set; }

        [Required]
        public DateTime PostedDate { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey(nameof(TopicEntity))]
        public int? TopicEntityId { get; set; }
        public TopicEntity? TopicEntity { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
