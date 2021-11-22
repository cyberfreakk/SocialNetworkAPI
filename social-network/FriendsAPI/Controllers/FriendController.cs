using FriendsService.Logger;
using FriendsService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(LoggingAspect))]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService service;

        public FriendController(IFriendService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("acceptrequest/{friendName}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult AcceptRequest( string friendName)
        {
            try
            {
                var userName = this.User.Identity.Name;
                service.acceptRequest(userName, friendName);
                return Ok("Friend Request Accepted!");
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            
        }

        [HttpDelete]
        [Route("deleterequest/{friendName}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult DeleteRequest( string friendName)
        {
            try
            {
                var userName = this.User.Identity.Name;
                service.deleteRequest(userName, friendName);
                return Ok("Friend Request Deleted!");
            }            
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpDelete]
        [Route("deletefriend/{friendName}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult DeleteFriend(string friendName)
        {
            try
            {
                var userName = this.User.Identity.Name;
                service.deleteFriend(userName, friendName);
                return Ok("Friend Deleted!");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet]
        [Route("getfriendlist")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult FriendList()
        {
            var userName = this.User.Identity.Name;

            return Ok(service.getFriendlistByUserName(userName));
        }

        [HttpGet]
        [Route("getfriendrequest")]
        [Authorize(Roles = "User, Admin")]

        public IActionResult getFriendRequests()
        {
            var userName = this.User.Identity.Name;
            return Ok(service.getFriendRequestByUserName(userName));
        }

        [HttpGet]
        [Route("isfriend/{friendName}")]

        [Authorize(Roles = "User, Admin")]
        public IActionResult IsFriend(string friendName)
        {
            var userName = this.User.Identity.Name;
            return Ok(service.isFriend(userName, friendName));
        }

        [HttpGet]
        [Route("getnumberoffriends")]
        [Authorize(Roles = "User, Admin")]

        public IActionResult GetNumberOfFriends()
        {
            var userName = this.User.Identity.Name;
            return Ok(service.numberOfFriends(userName));
        }

        [HttpGet]
        [Route("getnumberofrequests")]
        [Authorize(Roles = "User, Admin")]

        public IActionResult GetNumberOfRequests()
        {
            var userName = this.User.Identity.Name;
            return Ok(service.numberOfRequests(userName));
        }

        [HttpPost]
        [Route("sendrequest/{friendName}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult SendFriendRequest(string friendName)
        {
            var userName = this.User.Identity.Name;
            if (friendName == userName)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Recursive request not possible");
            }
            return service.sendRequest(userName, friendName)? 
                Ok("Friend Request Sent!"):
                StatusCode(StatusCodes.Status403Forbidden,$"{friendName} is already your friend");

        }
    }
}
