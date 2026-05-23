using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Service.Exceptions
{
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException() : base("Comment was't found in the database !")
        {
        }
    }
}
