using Commerce.Core.Pipelines.Examples;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Server.CatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipelineExampleController : Controller
    {
        private IPipelineExample _pipelineExample;

        public PipelineExampleController(IPipelineExample pipelineExample)
        {
            _pipelineExample = pipelineExample;
        }

        [HttpGet]
        public IActionResult Index(string str)
        {
            return Json(new
            {
                Result = _pipelineExample.Run(str, new object())
            });
        }    
    }
}