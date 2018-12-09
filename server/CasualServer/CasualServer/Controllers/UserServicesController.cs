using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasualServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasualServer.Controllers
{
    [Produces("application/json")]
    [Route("userServices")]
    public class UserServicesController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public UserServicesController(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public JsonResult GetUserServices()
        {
            var result = _dbContext.UserServices
                .Select(us => new
                {
                    UserServiceId = us.UserServiceId,
                    UserId = us.User.UserId,
                    ServiceId = us.Service.ServiceId
                })
                .ToList();


            return Json(result);
        }

        [Route("user")]
        [HttpGet("{value}")]
        public JsonResult GetUserServicesOfUser(string value)
        {
            var result = _dbContext.UserServices
                            .Where(us => us.User.Nickname == value)
                            .Select(us => new
                            {                                
                                ServiceName = us.Service.ServiceName
                            })
                            .ToList();


            return Json(result);
        }

        [HttpPost]
        public void AddUserServices([FromHeader]string service, [FromHeader]string user)
        {
            var u = _dbContext.Users.Find(Convert.ToInt32(user));
            var s = _dbContext.Services.Find(Convert.ToInt32(service));

            _dbContext.UserServices.Add(new UserService { Service = s, User = u });
            _dbContext.SaveChanges();
        }

        [HttpDelete("{value}")]
        public void DeleteUserService(string value)
        {
            var entity = _dbContext.UserServices.Where(us => us.User.Nickname == value).ToList();
            _dbContext.UserServices.RemoveRange(entity);
            _dbContext.SaveChanges();
        }

        [Route("userServices/user")]
        [HttpPut("{value}")]
        public void ChangeUserInUserService(string userName, [FromHeader]string service, [FromHeader]string newValue)
        {
            var entity = _dbContext.UserServices.Where(us => us.User.Nickname == userName && us.Service.ServiceName == service).ToList();
            var newUser = _dbContext.Users.Where(u => u.Nickname == newValue).First();
            foreach (var item in entity)
            {
                item.User = newUser;
            }
            _dbContext.SaveChanges();
        }

    }
}