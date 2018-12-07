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
    [Route("log")]
    public class LogController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public LogController(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public JsonResult GetLog()
        {
            var result = _dbContext.Log.Select(l => new
            {
                LogId = l.Id,
                Time = l.Time,
                ActionId = l.Action.ActionId
            }
                ).ToList();

            return Json(_dbContext.Log.ToList());
        }

        [HttpPost("actionType")]
        public void AddActionToLog(string actionType)
        {
            var action = _dbContext.Actions.Where(a => a.ActionType == actionType).First();

            _dbContext.Log.Add(new Log { Action = action, Time = DateTime.Now});
            _dbContext.SaveChanges();
        }

    }
}