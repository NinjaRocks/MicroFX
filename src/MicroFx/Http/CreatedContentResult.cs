using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MicroFx.Http
{
    public class CreatedContentResult<T> : BaseContentResult<T>
    {
       
        public Uri Location { get; }

        public CreatedContentResult(HttpRequestMessage request, Uri location, T content, string reason = null)
            :base(request, HttpStatusCode.Created, content, reason)
        {
            Location = location;
        }
      
        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = GetResponse();

            response.Headers.Location = Location;

            return Task.FromResult(response);
        }
    }
}