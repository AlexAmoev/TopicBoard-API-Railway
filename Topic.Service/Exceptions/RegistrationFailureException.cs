using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Service.Exceptions
{
    public class RegistrationFailureException : Exception
    {
        public RegistrationFailureException(string message) : base(message)
        {
        }
    }
}
