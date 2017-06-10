using System.Collections.Generic;
using MicroFx;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1
{
    public interface IBookShelfResource : IResource
    {
        IEnumerable<BookShelfDocument> Get();
        BookShelfDocument Get(int id);
        BookShelfDocument Update(int id, BookShelfDocument document);
        void Delete(int id);
        BookShelfDocument Post(BookShelfDocument document);
    }
}