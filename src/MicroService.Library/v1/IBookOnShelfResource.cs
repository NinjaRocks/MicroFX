using System.Collections.Generic;
using MicroFx;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1
{
    public interface IBookOnShelfResource : IResource
    {
        IEnumerable<BookOnShelfDocument> Get(int bookshelfId);
        BookOnShelfDocument Get(int bookshelfId, int bookId);
        void Delete(int bookshelfId, int bookId);
        BookOnShelfDocument Post(int bookshelfId, int bookId);
    }
}