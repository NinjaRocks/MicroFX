using System;
using System.Net;
using System.Net.Http;

namespace MicroFx.Http
{
    public static class HttpExtension
    {
        public static ContentResult<T> CreateContentResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T obj, string reasonPhrase = null)
        {
            return new ContentResult<T>(request, statusCode, obj, reasonPhrase);
        }

        public static ContentResult<T> CreateContentResponse<T>(this HttpRequestMessage request, T obj, string reasonPhrase = null)
        {
            return new ContentResult<T>(request, HttpStatusCode.OK, obj, reasonPhrase);
        }

        public static NoContentResult CreateNoContentResponse(this HttpRequestMessage request, string reasonPhrase = null)
        {
            return new NoContentResult(request, reasonPhrase);
        }

        public static CreatedContentResult<T> CreateNewContentResponse<T>(this HttpRequestMessage request, Uri location, T obj, string reasonPhrase = null)
        {
            return new CreatedContentResult<T>(request, location, obj, reasonPhrase);
        }
    }
}
