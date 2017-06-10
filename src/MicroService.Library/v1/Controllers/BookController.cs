using System;
using System.Collections.Generic;
using System.Web.Http;
using MicroFx.Http;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1.Controllers
{
    [RoutePrefix("v1/books")]
    public class BookController : ApiController
    {
        private readonly IBookResource resource;

        public BookController(IBookResource resource)
        {
            this.resource = resource;
        }

        [AllowAnonymous]
        [HttpGet, Route]
        public IHttpContentResult<IEnumerable<BookDocument>> Get()
        {
            return Request.CreateContentResponse(resource.Get());
        }

        [AllowAnonymous]
        [HttpGet, Route("{bookId}")]
        public IHttpContentResult<BookDocument> Get(int bookId)
        {
            return Request.CreateContentResponse(resource.Get(bookId));
        }

        [AllowAnonymous]
        [HttpPost, Route]
        public IHttpContentResult<BookDocument> Post(BookDocument document)
        {
            var result = resource.Create(document);
            return Request.CreateNewContentResponse(new Uri($"v1/books/{result.BookId}"), result);
        }

        [AllowAnonymous]
        [HttpPut, Route("{bookId}")]
        public IHttpContentResult<BookDocument> Put(int bookId, BookDocument document)
        {
            return Request.CreateContentResponse(resource.Update(bookId, document));
        }

        [AllowAnonymous]
        [HttpDelete, Route("{bookId}")]
        public IHttpActionResult Delete(int bookId)
        {
            resource.Delete(bookId);
            return Request.CreateNoContentResponse("book deleted");
        }
    }
}