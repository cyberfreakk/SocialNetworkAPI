using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Services
{
    public interface IUserService
    {
        public bool Register(ClaimsPrincipal user, User usermodel, string userRole);
        public object login(ClaimsPrincipal user, LoginModel model);
        public UserIdentity getUser(ClaimsPrincipal user,string username);
        public UserIdentity getUser(ClaimsPrincipal user);
        public bool signout();
        public bool changePassword(ClaimsPrincipal user, ChangePasswordModel model);

    }
}
