using System.Collections.Generic;
using MicroFx;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1
{
    public interface IBorrowerResource : IResource
    {
        IEnumerable<BorrowerDocument> Get();
        BorrowerDocument Get(int id);
        BorrowerDocument Update(int id, BorrowerDocument document);
        void Delete(int id);
        BorrowerDocument Post(BorrowerDocument document);
    }
}