using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasualServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasualServer.Controllers
{
    [Produces("application/json")]
    [Route("actions")]
    public class ActionsController : Controller
    {
        private readonly DatabaseContext _dbContext;
        public ActionsController(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public JsonResult GetActions()
        {
            var result = _dbContext.Actions
                .Select(action => new
                {
                    ActionId = action.ActionId,
                    ActionType = action.ActionType,
                    User = action.User.UserId,
                    Service = action.Service.ServiceId                    
                })
                .ToList();
            
                
            return Json(result);
        }

        [HttpGet("{value}")]
        public JsonResult GetActionsOfUser(string value)
        {
            var result = _dbContext.Actions
                .Where(a => a.User.UserId == Convert.ToInt32(value))
                .Select(action => new
                {
                    ActionId = action.ActionId,
                    ActionType = action.ActionType,
                    User = action.User.UserId,
                    Service = action.Service.ServiceId
                })
                .ToList();

            return Json(result);
        }

        [HttpPost]
        public void AddAction([FromHeader]string actionType, [FromHeader]string service, [FromHeader]string user)
        {
            var u = _dbContext.Users.Find(Convert.ToInt32(user));
            var s = _dbContext.Services.Find(Convert.ToInt32(service));
            //Console.WriteLine(value);
            
            _dbContext.Actions.Add(new Models.Action { ActionType = actionType, Service = s, User = u });
            _dbContext.SaveChanges();
        }

        [HttpDelete("{value}")]
        public void DeleteAction(string value)
        {
            var entity = _dbContext.Actions.Where(a => a.ActionType == value).ToList();
            _dbContext.Actions.RemoveRange(entity);
            _dbContext.SaveChanges();
        }

        [HttpPut("{value}")]
        public void ChangeAction(string value, [FromHeader]string newValue)
        {
            var entity = _dbContext.Actions.Where(ac => ac.ActionType == value).ToList();
            foreach (var item in entity)
            {
                item.ActionType = newValue;
            }
            _dbContext.SaveChanges();
        }
    }
}