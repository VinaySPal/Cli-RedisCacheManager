using System;
using CustomException;
using Newtonsoft.Json.Linq;
namespace EntityMetadataBL.Helper
{
    public static class JsonHelper
    {
        /// <summary>
        /// This method validate json. It is exposed to validate both json Array and Json Object
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="jsonType"></param>
        /// <returns>true for valid json otherwise throws exception</returns>
        public static bool ValidateJson(string jsonString, JsonType jsonType)
        {
            bool isValid = false;
            try
            { 
                if (String.IsNullOrEmpty(jsonString))
                    throw new HandledException("Json string is empty");

                JToken jsonToken = JContainer.Parse(jsonString);
                switch(jsonType)
                {
                    case JsonType.JArray:
                        isValid = checkForJsonArray(jsonToken);
                        break;
                    case JsonType.JObject:
                        isValid = checkForJsonObject(jsonToken);
                        break;
                }
            }
            catch (HandledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Provided json string is not valid", ex);
            }
            return isValid;
        }

        /// <summary>
        /// This method validates for Json Array
        /// </summary>
        /// <param name="jsonToken"></param>
        /// <returns>true for valid json otherwise throws exception</returns>
        private static bool checkForJsonArray(JToken jsonToken)
        {
            if (!(jsonToken is JArray))
                throw new HandledException("Provided json string is not valid Json Array");

            return true;
        }

        /// <summary>
        /// This method validates for Json Object
        /// </summary>
        /// <param name="jsonToken"></param>
        /// <returns>true for valid json otherwise throws exception</returns>
        private static bool checkForJsonObject(JToken jsonToken)
        {       
            if (!(jsonToken is JObject))
                throw new HandledException("Provided json string is not valid Json Object");

            return true;
        }
    }

    public enum JsonType
    {
        JObject, JArray
    }
}
