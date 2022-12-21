using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string FullName { get; }
    }
}
