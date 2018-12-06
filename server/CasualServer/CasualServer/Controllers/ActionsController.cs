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
            return Json(_dbContext.Actions.ToList());
        }

        [HttpGet("{value}")]
        public JsonResult GetActionsOfUser(string value)
        {



            return Json(value);
        }

        [HttpPost]
        public void AddAction([FromHeader]string actionType, int service, int user)
        {
            Service s = new Service { ServiceId = service };
            User u = new User { UserId = user };
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
        public void ChangeCategory(string value, [FromHeader]string newValue)
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