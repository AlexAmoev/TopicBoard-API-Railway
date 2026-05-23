using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;

namespace Topic.Contracts
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User appkicationUser, IEnumerable<string> roles);
    }
}
