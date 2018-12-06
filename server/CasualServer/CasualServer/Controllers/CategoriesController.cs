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
    [Route("categories")]
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public CategoriesController(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpGet]
        public JsonResult GetCategories()
        {
            return Json(_dbContext.Caterogies.ToList());
        }


        [HttpPost("{value}")]
        public void AddCategory(string value)
        {
            //Console.WriteLine(value);
            _dbContext.Caterogies.Add(new Category { CategoryName = value });
            _dbContext.SaveChanges();
        }

        [HttpDelete("{value}")]
        public void DeleteCategory(string value)
        {
            var entity = _dbContext.Caterogies.Where(c => c.CategoryName == value).ToList();
            _dbContext.Caterogies.RemoveRange(entity);
            _dbContext.SaveChanges();
        }

        [HttpPut("{value}")]
        public void ChangeCategory(string value, [FromHeader]string newValue)
        {
            var entity = _dbContext.Caterogies.Where(c => c.CategoryName == value).ToList();
            foreach(var item in entity)
            {
                item.CategoryName = newValue;                
            }
            _dbContext.SaveChanges();
        }
    }
}
