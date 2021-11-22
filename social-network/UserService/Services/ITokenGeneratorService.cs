using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Services
{
    public interface ITokenGeneratorService
    {
        public object Generate(UserIdentity user, IList<string> userRoles);
    }
}
