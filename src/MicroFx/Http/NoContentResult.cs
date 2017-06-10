using System.Net;
using System.Net.Http;

namespace MicroFx.Http
{
    public class NoContentResult : BaseStatusResult
    {
        public NoContentResult(HttpRequestMessage request, string reason= null)
            : this(request, HttpStatusCode.NoContent, reason)
        {
         
        }
        public NoContentResult(HttpRequestMessage request, HttpStatusCode statusCode, string reason = null)
           : base(request, statusCode, reason)
        {

        }
    }
}
