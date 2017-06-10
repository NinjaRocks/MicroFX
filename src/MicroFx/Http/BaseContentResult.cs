using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MicroFx.Http
{
    public abstract class BaseContentResult<T> : BaseStatusResult, IHttpContentResult<T>
    {
        protected BaseContentResult(HttpRequestMessage request, HttpStatusCode httpStatusCode, T content, string reasonPhrase)
            : base(request, httpStatusCode,reasonPhrase)
        {
            Content = content;
        }

       public T Content { get; }
       
        protected HttpResponseMessage GetResponse()
        {
            var response = ReasonPhrase != null
                ? Request.CreateResponse(HttpStatusCode, Content, ReasonPhrase)
                : Request.CreateResponse(HttpStatusCode, Content);

            return response;
        }

        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(GetResponse());
        }
    }
}