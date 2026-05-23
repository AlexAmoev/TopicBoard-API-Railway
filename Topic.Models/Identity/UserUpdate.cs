using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Models.Identity
{
    public class UserUpdate
    {
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
