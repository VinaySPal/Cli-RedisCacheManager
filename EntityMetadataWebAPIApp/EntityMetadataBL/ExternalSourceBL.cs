using CustomException;
using EntityMetadataBL.Helper;
using EntityMetadataWebAPI.Models.EntityMetadataFacadeModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace EntityMetadataBL
{
    public class ExternalSourceBL : HTTPOperatorBase
    {
        private readonly string _baseURI = null;
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient = null;
        public ExternalSourceBL( IConfiguration configuration)
        {
            _configuration = configuration;
            _baseURI = _configuration["External_Source_Fields_BaseURI"];
            _httpClient = CreateHttpClient();
        }

        protected override HttpClient CreateHttpClient()
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(_baseURI);                
            }
            return _httpClient;

        }

        /// <summary>
        /// This method returns the external fields which are fetched from 2 sources as datatable.
        /// </summary>
        /// <returns>datatable of fields</returns>
        public DataTable GetFieldDataTable()
        {
            Dictionary<string, List<string>> fieldDict = GetCombinedFields(GetDefaultFields(), GetCustomFields());
            return DataTableHelper.ParseDictToDataTable(fieldDict);
        }

        /// <summary>
        /// This method returns the fileds as json string from /api/DefaultFields
        /// </summary>
        /// <returns>json string of default fileds</returns>
        private string GetDefaultFields()
        {
            string resourceURI = "/api/DefaultFields";
            string sourceFieldJson = string.Empty;
            try 
            {
                HttpResponseMessage httpResponse = base.Get(resourceURI);
                sourceFieldJson = httpResponse.Content.ReadAsStringAsync().Result.ToString();
            }
            catch (Exception ex)
            {
                throw new HandledException($"{resourceURI} didn't return valid field json.", ex);
            }
            JsonHelper.ValidateJson(sourceFieldJson, JsonType.JObject);
            return sourceFieldJson;
        }

        /// <summary>
        /// This method returns the fileds as json string from /api/CustomFields
        /// </summary>
        /// <returns>json string of Custom fileds</returns>
        private string GetCustomFields()
        {
            string resourceURI = "/api/CustomFields";
            string customFieldJson = string.Empty;
            try
            {
                HttpResponseMessage httpResponse = base.Get(resourceURI);
                customFieldJson = httpResponse.Content.ReadAsStringAsync().Result.ToString();
            }
            catch (Exception ex)
            {
                throw new HandledException($"{resourceURI} didn't return valid field json.", ex);
            }
            JsonHelper.ValidateJson(customFieldJson, JsonType.JObject);
            return customFieldJson;
        }

        /// <summary>
        /// This method combines both the json string and return it as Dictionary<string, List<string>>
        /// </summary>
        /// <param name="sourceFieldJson"></param>
        /// <param name="customFieldJson"></param>
        /// <returns>Dictionary<string, List<string>></returns>
        private Dictionary<string, List<string>> GetCombinedFields(string sourceFieldJson, string customFieldJson)
        {
            try
            { 
                var sourceFieldDict = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(sourceFieldJson);
                var customFieldDict = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(customFieldJson);
                List<string> fieldList = new List<string>();
                customFieldDict.TryGetValue(SourceFields.Fields.ToString(), out fieldList);
                sourceFieldDict[SourceFields.Fields.ToString()].AddRange(fieldList);
                return sourceFieldDict;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetCombinedFields: " + ex.Message);
                throw new Exception("Source fields are not compatible to each other.");
            }
        }

    }
}
