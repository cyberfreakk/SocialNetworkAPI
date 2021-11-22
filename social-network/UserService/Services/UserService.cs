using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Repository;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repo;
        private readonly ITokenGeneratorService service;
        public UserService(IUserRepository repo, ITokenGeneratorService service)
        {
            this.repo = repo;
            this.service = service;
        }

        public bool changePassword(ClaimsPrincipal user, ChangePasswordModel model)
        {
            var task= repo.changePassword(user, model);
            task.Wait();
            return task.Result;
        }

        public UserIdentity getUser(ClaimsPrincipal user,string username)
        {
            var task=repo.getUser(user,username);
            task.Wait();
            return task.Result;
        }

        public UserIdentity getUser(ClaimsPrincipal user)
        {
            var task = repo.getUser(user);
            task.Wait();
            return task.Result;
        }

        public object login(ClaimsPrincipal user, LoginModel model)
        {
            var task = repo.login(user,model);
            task.Wait();
            var res= task.Result;
            return service.Generate(res.Item1, res.Item2);
        }

        public bool Register(ClaimsPrincipal user, User usermodel, string userRole)
        {
            var task = repo.Register(user, usermodel,userRole);
            task.Wait();
            return task.Result;
        }

        public bool signout()
        {
            var task = repo.signout();
            task.Wait();
            return task.Result;
        }
    }
}
