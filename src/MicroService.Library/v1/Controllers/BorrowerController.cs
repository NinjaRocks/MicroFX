using System;
using System.Collections.Generic;
using System.Web.Http;
using MicroFx.Http;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1.Controllers
{
    [RoutePrefix("v1/borrowers")]
    public class BorrowerController : ApiController
    {
        private readonly IBorrowerResource resource;

        public BorrowerController(IBorrowerResource resource)
        {
            this.resource = resource;
        }

        [AllowAnonymous]
        [HttpGet, Route]
        public IHttpContentResult<IEnumerable<BorrowerDocument>> Get()
        {
            return Request.CreateContentResponse(resource.Get());
        }

        [AllowAnonymous]
        [HttpGet, Route("{borrowerId}")]
        public IHttpContentResult<BorrowerDocument> Get(int borrowerId)
        {
            return Request.CreateContentResponse(resource.Get(borrowerId));
        }

        [AllowAnonymous]
        [HttpPost, Route]
        public IHttpContentResult<BorrowerDocument> Post(BorrowerDocument document)
        {
            var result = resource.Post(document);
            return Request.CreateNewContentResponse(new Uri($"v1/borrowers/{result.BorrowerId}"), result);
        }

        [AllowAnonymous]
        [HttpPut, Route("{borrowerId}")]
        public IHttpContentResult<BorrowerDocument> Put(int borrowerId, BorrowerDocument document)
        {
            return Request.CreateContentResponse(resource.Update(borrowerId, document));
        }

        [AllowAnonymous]
        [HttpDelete, Route("{borrowerId}")]
        public IHttpActionResult Delete(int borrowerId)
        {
            resource.Delete(borrowerId);
            return Request.CreateNoContentResponse();
        }
    }
}