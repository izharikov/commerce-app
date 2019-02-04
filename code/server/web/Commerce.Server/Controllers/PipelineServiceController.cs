using Commerce.Core.Mvc.Controllers;
using Commerce.Core.Pipelines.Service;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Server.Controllers
{
    public class PipelineServiceController : AdminApiCommerceController
    {
        private readonly IPipelineService _pipelineService;

        public PipelineServiceController(IPipelineService pipelineService)
        {
            _pipelineService = pipelineService;
        }

        [HttpGet]
        [Route("GetAllServices")]
        public IActionResult GetAllServices()
        {
            return Json(_pipelineService.GetRegisteredPipelinesJson());
        }
    }
}