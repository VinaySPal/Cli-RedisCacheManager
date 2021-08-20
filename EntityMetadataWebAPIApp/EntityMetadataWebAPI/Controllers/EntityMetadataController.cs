using EntityMetadataBL;
using EntityMetadataWebAPI.Models.EntityMetadataFacadeModel;
using EntityMetadataWebAPI.Models.UIModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;


namespace EntityMetadataWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityMetadataController : ControllerBase
    {
        private readonly EntityConfigBL _objEntityConfigBL = null;
        private readonly ILogger<EntityMetadataController> _logger;
        public EntityMetadataController(EntityConfigBL objEntityConfigBL, ILogger<EntityMetadataController> logger)
        {
            _objEntityConfigBL = objEntityConfigBL;
            _logger = logger;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public ResponseData<IEnumerable<EntityMetadataModel>> Get()
        {
            ResponseData<IEnumerable<EntityMetadataModel>> objResponseData = new ResponseData<IEnumerable<EntityMetadataModel>>();
            try
            {
                objResponseData.Data = _objEntityConfigBL.GetEntityMetadata();
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Exception Msg: " + ex.Message);
                _logger.LogInformation("Stack Trace: " + ex.StackTrace);
                _logger.LogInformation("Inner Exception Msg: " + ex.InnerException);
                objResponseData.ErrorInformation = ex.Message;
                objResponseData.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return objResponseData;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ResponseData<int> Post([FromBody] string jsonString)
        {
            ResponseData<int> objResponseData = new ResponseData<int>();
            try
            {
                objResponseData.Data = _objEntityConfigBL.SaveEntityConfigs(jsonString);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Msg: " + ex.Message);
                _logger.LogInformation("Stack Trace: " + ex.StackTrace);
                _logger.LogInformation("Inner Exception Msg: " + ex.InnerException);
                objResponseData.ErrorInformation = ex.Message;
                objResponseData.HttpStatusCode = HttpStatusCode.BadRequest;
            }
            return objResponseData;
        }
    }
}


/*
 
        public HttpResponseMessage Get()
        {
            ResponseData<IEnumerable<EntityMetadataModel>> objResponseData = new ResponseData<IEnumerable<EntityMetadataModel>>();
            try
            {
                objResponseData.Data = _objEntityConfigBL.GetEntityMetadata();
            }
            catch(Exception ex)
            {
                return CustomJsonResult.JsonResult(new ResponseData<string> { ErrorInformation = ex.Message, HttpStatusCode = HttpStatusCode.BadRequest });
            }
            return CustomJsonResult.JsonResult(objResponseData);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] string jsonString)
        {
            ResponseData<int> objResponseData = new ResponseData<int>();
            try
            {
                objResponseData.Data = _objEntityConfigBL.SaveEntityConfigs(jsonString);
            }
            catch (Exception ex)
            {
                return CustomJsonResult.JsonResult(new ResponseData<string> { ErrorInformation = ex.Message, HttpStatusCode = HttpStatusCode.BadRequest });
            }
            return CustomJsonResult.JsonResult(objResponseData);
        }
 */