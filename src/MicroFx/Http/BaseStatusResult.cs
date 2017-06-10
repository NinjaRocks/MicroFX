using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MicroFx.Http
{
    public abstract class BaseStatusResult : IHttpStatusResult
    {
        protected BaseStatusResult(HttpRequestMessage request, HttpStatusCode statusCode, string reason = null)
        {
            Request = request;
            ReasonPhrase = reason;
            HttpStatusCode = statusCode;
        }

        public HttpRequestMessage Request { get; }
        public HttpStatusCode HttpStatusCode { get; }
        public string ReasonPhrase { get; }


        public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = ReasonPhrase != null
                ? Request.CreateResponse(HttpStatusCode, ReasonPhrase)
                : Request.CreateResponse(HttpStatusCode);

            return Task.FromResult(response);
        }
    }
}