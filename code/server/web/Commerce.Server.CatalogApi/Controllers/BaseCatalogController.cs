using Commerce.Core.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Server.CatalogApi.Controllers
{
    public class BaseCatalogController : ApiCommerceController
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