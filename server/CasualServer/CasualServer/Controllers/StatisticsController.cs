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
    [Route("statistics")]
    public class StatisticsController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public StatisticsController(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [Route("usersreg")]
        [HttpGet]
        public JsonResult GetUsersReg()
        {
            var result = _dbContext.Log.Where(l =>  l.Time.Year == DateTime.Now.Year && l.Action.ActionType == "register").Count();
            return Json(result);
        }

        [Route("servicepay")]
        [HttpGet]
        public JsonResult GetServicePay()
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            var servicesId = _dbContext.Services.Select(s => new
            {
                s.ServiceId
            }).ToList();
            List<int> listId = new List<int>();
            foreach(var elem in servicesId)
            {
                listId.Add(elem.ServiceId);
            }
            foreach (var id in listId)
            {
                result[id] = _dbContext.Actions.Count(a => a.Service.ServiceId == id && a.ActionType == "payment is made");
            }            
            var max = result.First(index => index.Value == result.Values.Max()).Key;
            var response = _dbContext.Services.Where(s => s.ServiceId == max);
            
            return Json(response);
        }

        [Route("userpay")]
        [HttpGet]
        public JsonResult GetUserPay()
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            var usersId = _dbContext.Users.Select(u => new
            {
                u.UserId
            }).ToList();
            List<int> listId = new List<int>();
            foreach (var elem in usersId)
            {
                listId.Add(elem.UserId);
            }
            foreach (var id in listId)
            {
                result[id] = _dbContext.Actions.Count(a => a.User.UserId == id && a.ActionType == "payment is made");
            }
            var max = result.First(index => index.Value == result.Values.Max()).Key;
            var response = _dbContext.Users.Where(u => u.UserId == max).First();

            return Json(response);
        }
    }
}