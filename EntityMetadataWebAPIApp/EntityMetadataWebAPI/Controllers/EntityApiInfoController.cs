using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityMetadataWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntityApiInfoController : ControllerBase
    {
        private readonly ILogger<EntityApiInfoController> _logger;

        public EntityApiInfoController(ILogger<EntityApiInfoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            string apiInfo = "POST http://localhost:5170/api/EntityMetadata => (It SAVE Entity Configuration passed in body) \nGET http://localhost:5170/api/EntityMetadata => (It returns Entity Configuration in body)";
            return apiInfo;
        }
    }
}
