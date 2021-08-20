using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EntityMetadataBL.Helper
{
    public abstract class HTTPOperatorBase
    {
        private HttpResponseMessage _response = null;
        public HTTPOperatorBase()
        {
            _response = new HttpResponseMessage();
        }

        protected abstract HttpClient CreateHttpClient();

        protected HttpResponseMessage Get(string resourceURI)
        {
            _response = CreateHttpClient().GetAsync(resourceURI).Result;
            return _response;
        }

        protected HttpResponseMessage Post(string resourceURI, HttpContent bodyContent)
        {
            _response = CreateHttpClient().PostAsync(resourceURI, bodyContent).Result;
            return _response;
        }

        protected HttpResponseMessage Delete(string resourceURI)
        {
            try
            {
                _response = CreateHttpClient().DeleteAsync(resourceURI).Result;
            }
            catch (Exception)
            {
                throw;
            }
            return _response;
        }
    }
}
