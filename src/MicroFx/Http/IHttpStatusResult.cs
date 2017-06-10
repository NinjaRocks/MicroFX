using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MicroFx.Http
{
    public interface IHttpStatusResult : IHttpActionResult
    {
        HttpStatusCode HttpStatusCode { get; }
        string ReasonPhrase { get; }
        HttpRequestMessage Request { get; }
    }
}