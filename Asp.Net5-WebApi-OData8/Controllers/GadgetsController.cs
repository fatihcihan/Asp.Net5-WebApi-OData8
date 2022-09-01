using Asp.Net5_WebApi_OData8.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Asp.Net5_WebApi_OData8.Controllers
{
    [Route("gadget")]
    [ApiController]
    public class GadgetsController : ControllerBase
    {
        private readonly EfContext _efContext;
        public GadgetsController(EfContext efContext)
        {
            _efContext = efContext;
        }

        [HttpGet("get")]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_efContext.Gadgets.AsQueryable());
        }
    }
}
