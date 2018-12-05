using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasualServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasualServer.Controllers
{
    [Route("/")]
    public class ValuesController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public ValuesController(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public JsonResult GetCategories()
        {
            return Json(_dbContext.Caterogies.ToList());
        }       

    }
}
