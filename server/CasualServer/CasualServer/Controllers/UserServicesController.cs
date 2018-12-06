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
            return Json(_dbContext.UserServices.ToList());
        }

    }
}