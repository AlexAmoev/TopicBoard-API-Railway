using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Models.Identity
{
    public class UserInfo
    {
        public string? Id { get; set; }
        public string? Role { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Topics { get; set; }
        public int? Comments { get; set; }
        public bool Banned { get; set; }
    }
}
