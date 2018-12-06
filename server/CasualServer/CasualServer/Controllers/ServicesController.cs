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
    [Route("services")]
    public class ServicesController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public ServicesController(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public JsonResult GetServices()
        {
            return Json(_dbContext.Services.ToList());
        }

    }
}