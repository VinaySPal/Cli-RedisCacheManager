using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EntityMetadataBL.Helper
{
    public abstract class HTTPOperatorBase
    {
        private HttpClient _httpClient = null;
        private HttpResponseMessage _response = null;
        public HTTPOperatorBase()
        {
            _httpClient = CreateHttpClient();
            _response = new HttpResponseMessage();
        }

        protected abstract HttpClient CreateHttpClient();

        protected HttpResponseMessage Get(string resourceURI)
        {
            _response = _httpClient.GetAsync(resourceURI).Result;
            return _response;
        }

        protected HttpResponseMessage Post(string resourceURI, HttpContent bodyContent)
        {
            _response = _httpClient.PostAsync(resourceURI, bodyContent).Result;
            return _response;
        }

        protected HttpResponseMessage Delete(string resourceURI)
        {
            try
            {
                _response = _httpClient.DeleteAsync(resourceURI).Result;
            }
            catch (Exception)
            {
                throw;
            }
            return _response;
        }
    }
}
