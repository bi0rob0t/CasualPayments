﻿using System;
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

        [HttpPost]
        public void AddUser([FromHeader]string nickname, [FromHeader]string login, [FromHeader]string password)
        {

            _dbContext.Users.Add(new User { Login = login, Nickname = nickname, Password = password });
            _dbContext.SaveChanges();
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
    }
}