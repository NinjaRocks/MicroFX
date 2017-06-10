using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace MicroFx.Configuration.Http
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is ResourceNotFoundException)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "ResourceNotFoundException"
                };

                context.Result = new ErrorMessageResult(context.Request, resp);
            }
            else if (context.Exception is ValidationException)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "ValidationException"
                };

                context.Result = new ErrorMessageResult(context.Request, resp);
            }
            else if (context.Exception is BusinessException)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "BusinessException"
                };

                context.Result = new ErrorMessageResult(context.Request, resp);
            }
        }

        public class ErrorMessageResult : IHttpActionResult
        {
            private readonly HttpRequestMessage request;
            private readonly HttpResponseMessage httpResponseMessage;


            public ErrorMessageResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage)
            {
                this.request = request;
                this.httpResponseMessage = httpResponseMessage;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(httpResponseMessage);
            }
        }
    }
}