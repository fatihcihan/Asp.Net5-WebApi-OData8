using Asp.Net5_WebApi_OData8.Data.Context;
using Asp.Net5_WebApi_OData8.Data.Entities;
using Microsoft.AspNetCore.OData;
//using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;

namespace Asp.Net5_WebApi_OData8.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[ODataRoutePrefix(nameof(Gadget))]
    public class GadgetsOdataController : ControllerBase
    {
        private readonly EfContext _efContext;
        public GadgetsOdataController(EfContext efContext)
        {
            _efContext = efContext;
        }

        [EnableQuery]
        [HttpGet, Produces("application/json")]
        public IQueryable<Gadget> Get()
        {
            return _efContext.Gadgets.AsQueryable();
        }

        [HttpGet]
        [EnableQuery(AllowedQueryOptions = Microsoft.AspNetCore.OData.Query.AllowedQueryOptions.All)]
        public SingleResult<Gadget> Get(int key)
        {
            var data = _efContext.Gadgets.Where(x => x.Id == key);
            return SingleResult.Create<Gadget>(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Gadget gadget)
        {
            var data = _efContext.Add(gadget);
            _efContext.SaveChanges();
            return Ok(data);
        }

        [HttpPatch]
        public IActionResult Patch(int key, [FromBody] Delta<Gadget> gadget)
        {
            var entity = _efContext.Gadgets.FirstOrDefault(x => x.Id == key);            

            gadget.Patch(entity);
            var data =  _efContext.Update(entity);
            _efContext.SaveChanges();
            return Ok(data);
            //if (data.Success) return Ok(data.Message); return BadRequest(data.Message);
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var data = _efContext.Gadgets.FirstOrDefault(x => x.Id == key);
            _efContext.Remove(data);
            _efContext.SaveChanges();
            return Ok(data);
        }

    }
}