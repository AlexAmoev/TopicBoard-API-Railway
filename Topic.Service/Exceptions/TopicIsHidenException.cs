using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Service.Exceptions
{
    internal class TopicIsHidenException : Exception
    {
        public TopicIsHidenException() : base("Topic is hiden !")
        {
        }
    }
}
