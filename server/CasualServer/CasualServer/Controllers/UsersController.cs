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
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public UsersController(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public JsonResult GetUsers()
        {
            return Json(_dbContext.Users.ToList());
        }

        [Route("nickname")]
        [HttpGet]
        public JsonResult GetUsers([FromHeader]string username)
        {
            return Json(_dbContext.Users.Where(u => u.Nickname == username).First());
        }


        [HttpPost]
        public bool AddUser([FromHeader]string nickname, [FromHeader]string login, [FromHeader]string password)
        {

            var entity = _dbContext.Users.Where(u => u.Login == login || u.Nickname == nickname).ToList();
            if (entity.Count == 0)
            {
                _dbContext.Users.Add(new User { Login = login, Nickname = nickname, Password = password });
                _dbContext.SaveChanges();
                return true;
            }                
            else
            {
                return false;
            }
            
            

        }

        [HttpDelete("{value}")]
        public void DeleteUser(string value)
        {
            var entity = _dbContext.Users.Where(u => u.Nickname == value).ToList();
            _dbContext.Users.RemoveRange(entity);
            _dbContext.SaveChanges();
        }

        [HttpPut("{value}")]
        public void ChangeNicknameUser(string value, [FromHeader]string newValue)
        {
            var entity = _dbContext.Users.Where(u => u.Nickname == value).ToList();
            foreach (var item in entity)
            {
                item.Nickname = newValue;
            }
            _dbContext.SaveChanges();
        }

        [Route("check")]
        [HttpGet]
        public bool CheckAuthOfLogPass([FromHeader]string login, [FromHeader]string password)
        {
            try
            {
                var entity = _dbContext.Users.Where(u => u.Login == login && u.Password == password).First();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}