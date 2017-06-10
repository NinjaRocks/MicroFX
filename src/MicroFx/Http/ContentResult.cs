using System.Net;
using System.Net.Http;

namespace MicroFx.Http
{
    public class ContentResult<T> : BaseContentResult<T>
    {
       public ContentResult(HttpRequestMessage request, HttpStatusCode statusCode, T content, string reason)
            :base(request, statusCode, content, reason)
        {
            
        }
        

        public ContentResult(HttpRequestMessage request,HttpStatusCode statusCode, T content)
            : this(request, statusCode, content, null)
        {
        }

        public ContentResult(HttpRequestMessage request, T content)
            : this(request, HttpStatusCode.OK, content)
        {
        }
    }
}