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
    [Route("services")]
    public class ServicesController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public ServicesController(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [Route("all")]
        [HttpGet]
        public JsonResult GetServices()
        {
            var result = _dbContext.Services
                .Select(service => new
                {
                    ServiceId = service.ServiceId,
                    ServiceName = service.ServiceName,
                    CategoryId = service.Category.CategoryId
                })
                .ToList();
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetServicesOfCategory([FromHeader] string desiredCategoryName)
        {            
            var result = _dbContext.Services                                
                .Select(service => new
                {
                    ServiceId = service.ServiceId,
                    ServiceName = service.ServiceName,
                    CategoryName = service.Category.CategoryName

                })
                .Where(c => c.CategoryName == desiredCategoryName)
                .ToList();
            return Json(result);
        }


        [HttpPost]
        public void AddService([FromHeader]string serviceName, [FromHeader]string categoryName)
        {
           var category = _dbContext.Caterogies.Where(c => c.CategoryName == categoryName).First();
            _dbContext.Services.Add(new Service { ServiceName = serviceName, Category = category });
            _dbContext.SaveChanges();
        }

        [HttpDelete("{value}")]
        public void DeleteService(string value)
        {
            var entity = _dbContext.Services.Where(s => s.ServiceName == value).ToList();
            _dbContext.Services.RemoveRange(entity);
            _dbContext.SaveChanges();
        }

        [Route("services/name")]
        [HttpPut("{value}")]
        public void ChangeNameService(string value, [FromHeader]string newValue)
        {
            var entity = _dbContext.Services.Where(s => s.ServiceName == value).ToList();
            foreach (var item in entity)
            {
                item.ServiceName = newValue;
            }
            _dbContext.SaveChanges();
        }

        [Route("services/category")]
        [HttpPut("{value}")]
        public void ChangeCategoryService(string value, [FromHeader]string newValue)
        {
            var entity = _dbContext.Services.Where(s => s.ServiceName == value).ToList();
            var category = _dbContext.Caterogies.Where(c => c.CategoryName == newValue).First();

            foreach (var item in entity)
            {               
                item.Category = category;
            }
            _dbContext.SaveChanges();
        }

    }
}