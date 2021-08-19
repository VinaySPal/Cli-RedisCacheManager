using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EntityMetadataWebAPI.Models.UIModel
{
    public class ResponseData<T>
    {
        public HttpStatusCode HttpStatusCode
        { get; set; } = HttpStatusCode.OK;

        public bool IsValidRequest { get { return (string.IsNullOrEmpty(this.ErrorInformation)); } }
        public string ErrorInformation { get; set; }

        public T Data { get; set; }

    }
}
