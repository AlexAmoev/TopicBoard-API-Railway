using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Service.Exceptions
{
    public class TopicNotFoundException : Exception
    {
        public TopicNotFoundException() : base("Topic was't found in the database !")
        {
        }
    }
}
