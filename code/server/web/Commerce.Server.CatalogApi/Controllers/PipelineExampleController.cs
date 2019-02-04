using Commerce.Core.Mvc.Controllers;
using Commerce.Core.Pipelines.Examples;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Server.CatalogApi.Controllers
{
    public class PipelineExampleController : ApiCommerceController
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