using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserService.DBContext;
using UserService.Model;
using UserService.Model.Friend;
using UserService.Services;

namespace UserService.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<UserIdentity> userManager;
        private readonly SignInManager<UserIdentity> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly MongoDbContext context;
        public UserRepository(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager, RoleManager<IdentityRole> roleManager, MongoDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public async Task<bool> changePassword(ClaimsPrincipal user, ChangePasswordModel model)
        {
            var profile = await userManager.FindByNameAsync(model.UserName);
            if (profile != null && signInManager.IsSignedIn(user))
            {
                var task = await userManager.ChangePasswordAsync(profile, model.OldPassword, model.NewPassword);
                return task.Succeeded;
            }
            else
            {
                return false;
            }
        }

        public async Task<UserIdentity> getUser(ClaimsPrincipal user,string username)
        {
            var profile = await userManager.FindByNameAsync(username);
            if (profile != null && signInManager.IsSignedIn(user))
            {

                return profile;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserIdentity> getUser(ClaimsPrincipal user)
        {
            var profile = await userManager.GetUserAsync(user);
            if (profile != null && signInManager.IsSignedIn(user))
            {

                return profile;
            }
            else
            {
                return null;
            }
        }

        public async Task<Tuple<UserIdentity,IList<string>>> login(ClaimsPrincipal user, LoginModel model)
        {
            if (signInManager.IsSignedIn(user) && user.Identity.Name != model.UserName)
            {
                throw new Exception("You have to signout first to perform this operation");
            }
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                
                var user_identity = await userManager.FindByNameAsync(model.UserName);
                var userRoles = await userManager.GetRolesAsync(user_identity);

                return Tuple.Create(user_identity,userRoles);
            }
            else
            {
                throw new Exception("Invalid username or password");
            }
        }

        public async Task<bool> Register(ClaimsPrincipal user, User usermodel,  string userRole)
        {
            if (signInManager.IsSignedIn(user))
            {
                throw new Exception("You have to signout first to perform this operation");
            }
            var userExists = await userManager.FindByNameAsync(usermodel.UserName);
            if (userExists != null)
                throw new Exception("User already exists!");

            UserIdentity user_identity = new UserIdentity()
            {
                Email = usermodel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = usermodel.UserName,
                FirstName = usermodel.FirstName,
                LastName = usermodel.LastName,
                Gender = usermodel.Gender,
                DateOfBirth = usermodel.DOB,
                PhoneNumber = usermodel.ContactNo

            };
            var result = await userManager.CreateAsync(user_identity, usermodel.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User creation failed! Please check user details and try again.");
            }

            if (!await roleManager.RoleExistsAsync(userRole))
            {
                await roleManager.CreateAsync(new IdentityRole(userRole));
            }

            await userManager.AddToRoleAsync(user_identity, userRole);
            await context.UserFriends.InsertOneAsync(new UserFriend()
            {
                UserName = usermodel.UserName,
                Friends = null,
                Requests = null
            });

            return true;
        }

        

        public async Task<bool> signout()
        {
            await signInManager.SignOutAsync();
            return true;
        }
    }
}
