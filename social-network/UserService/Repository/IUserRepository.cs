using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        public Task<bool> Register(ClaimsPrincipal user, User usermodel, string userRole);
        public  Task<Tuple<UserIdentity, IList<string>>> login(ClaimsPrincipal user, LoginModel model);
        public Task<UserIdentity> getUser(ClaimsPrincipal user,string username);
        public Task<UserIdentity> getUser(ClaimsPrincipal user);
        public Task<bool> signout();
        public  Task<bool> changePassword(ClaimsPrincipal user, ChangePasswordModel model);

    }
}
