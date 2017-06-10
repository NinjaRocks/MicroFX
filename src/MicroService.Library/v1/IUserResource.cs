using System.Collections.Generic;
using MicroFx;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1
{
    public interface IUserResource : IResource
    {
        UserDocument Get(int userId);
        UserDocument Update(int userId, UserDocument document);
        void Delete(int userId);
        UserDocument Post(UserDocument document);
        IEnumerable<UserDocument> Get(string userId, string password);
    }
}