using System.Collections.Generic;
using System.Web.Http;
using MicroFx.Http;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1.Controllers
{
    [RoutePrefix("v1/bookshelves/{bookshelfId}/books")]
    public class BookOnShelfController : ApiController
    {
        private readonly IBookOnShelfResource resource;

        public BookOnShelfController(IBookOnShelfResource resource)
        {
            this.resource = resource;
        }

        [AllowAnonymous]
        [HttpGet, Route]
        public IHttpContentResult<IEnumerable<BookOnShelfDocument>> Get(int bookshelfId)
        {
            return Request.CreateContentResponse(resource.Get(bookshelfId));
        }

        [AllowAnonymous]
        [HttpGet, Route("{bookId}")]
        public IHttpContentResult<BookOnShelfDocument> Get(int bookshelfId, int bookId)
        {
            return Request.CreateContentResponse(resource.Get(bookshelfId, bookId));
        }

       
        [AllowAnonymous]
        [HttpPost, Route("{bookId}")]
        public IHttpContentResult<BookOnShelfDocument> Post(int bookshelfId, int bookId)
        {
            var output = resource.Post(bookshelfId, bookId);
            return Request.CreateNewContentResponse(Request.RequestUri, output);
        }

       
        [AllowAnonymous]
        [HttpDelete, Route("{bookId}")]
        public IHttpActionResult Delete(int bookshelfId, int bookId)
        {
            resource.Delete(bookshelfId, bookId);
            return Request.CreateNoContentResponse();
        }
    }
}