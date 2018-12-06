using Commerce.Core.Pipelines.Service;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipelineServiceController : Controller
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