using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApiAngularAdDemo.DataComponents.Response
{
    public class Response<T>
    {
        public T Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }

        public Response()
        {
            this.Status = HttpStatusCode.InternalServerError;
        }
    }

    public enum ResponseStatus
    {
        Success,
        Failure,
        Warning
    }
}
