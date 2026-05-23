using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Contracts
{
    public interface ISaveble
    {
        Task Save();
    }
}
