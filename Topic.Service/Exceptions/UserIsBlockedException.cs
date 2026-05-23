using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Service.Exceptions
{
    public class UserIsBlockedException : Exception
    {
        public UserIsBlockedException() : base("User is blocked !")
        {
        }
    }
}
