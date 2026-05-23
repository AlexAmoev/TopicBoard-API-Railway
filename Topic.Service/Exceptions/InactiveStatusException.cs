using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Service.Exceptions
{
    public class InactiveStatusException : Exception
    {
        public InactiveStatusException() : base("This post is viewable only. He was assigned the status 'Inactive' due to long inactivity. Contact the administration for assistance.")
        {
        }
    }
}
