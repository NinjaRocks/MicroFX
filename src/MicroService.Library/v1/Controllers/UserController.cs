using System;
using System.Collections.Generic;
using System.Web.Http;
using MicroFx.Http;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1.Controllers
{
    [RoutePrefix("v1/users")]
    public class UserController : ApiController
    {
        private readonly IUserResource resource;

        public UserController(IUserResource resource)
        {
            this.resource = resource;
        }

        [AllowAnonymous]
        [HttpGet, Route]
        public IHttpContentResult<IEnumerable<UserDocument>> Get([FromUri] string email, [FromUri] string password)
        {
            return Request.CreateContentResponse(resource.Get(email, password));
        }

        [AllowAnonymous]
        [HttpGet, Route("{userId}")]
        public IHttpContentResult<UserDocument> Get(int userId)
        {
            return Request.CreateContentResponse(resource.Get(userId));
        }

        [AllowAnonymous]
        [HttpPost, Route]
        public IHttpContentResult<UserDocument> Post(UserDocument document)
        {
            var result = resource.Post(document);
            return Request.CreateNewContentResponse(new Uri($"v1/users/{result.UserId}"), result);
        }

        [AllowAnonymous]
        [HttpPut, Route("{userId}")]
        public IHttpContentResult<UserDocument> Put(int userId, UserDocument document)
        {
            return Request.CreateContentResponse(resource.Update(userId, document));
        }

        [AllowAnonymous]
        [HttpDelete, Route("{userId}")]
        public IHttpActionResult Delete(int userId)
        {
            resource.Delete(userId);
            return Request.CreateNoContentResponse();
        }
    }
}