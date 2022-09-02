using Asp.Net5_WebApi_OData8.Data.Context;
using Asp.Net5_WebApi_OData8.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using System.Linq;

namespace Asp.Net5_WebApi_OData8.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly EfContext _efContext;
        public CategoriesController(EfContext efContext)
        {
            _efContext = efContext;
        }

        [EnableQuery]
        [HttpGet, Produces("application/json")]
        public IQueryable<Category> Get()
        {
            return _efContext.Categories.AsQueryable();
        }

        [EnableQuery(AllowedQueryOptions = Microsoft.AspNetCore.OData.Query.AllowedQueryOptions.All)]
        [HttpGet]
        public IQueryable<Category> GetExample()
        {
            return _efContext.Categories.AsQueryable();
        }

        [HttpGet]
        [EnableQuery(AllowedQueryOptions = Microsoft.AspNetCore.OData.Query.AllowedQueryOptions.All)]
        public SingleResult<Category> Get(int key)
        {
            var data = _efContext.Categories.Where(x => x.Id == key);
            return SingleResult.Create<Category>(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            var data = _efContext.Add(category);
            _efContext.SaveChanges();
            return Ok(data);
        }

        [HttpPatch]
        public IActionResult Patch(int key, [FromBody] Delta<Category> category)
        {
            var entity = _efContext.Categories.FirstOrDefault(x => x.Id == key);

            category.Patch(entity);
            var data = _efContext.Update(entity);
            _efContext.SaveChanges();
            return Ok(data);
            //if (data.Success) return Ok(data.Message); return BadRequest(data.Message);
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var data = _efContext.Categories.FirstOrDefault(x => x.Id == key);
            _efContext.Remove(data);
            _efContext.SaveChanges();
            return Ok(data);
        }
    }
}
