using Microsoft.AspNetCore.Mvc;

namespace Commerce.Server.CatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseCatalogController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return Json(new
            {
                SomeData = "example"
            });
        }
    }
}