using System.Collections.Generic;
using MicroFx;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1
{
    public interface IBookCheckoutResource : IResource
    {
        IEnumerable<BookCheckoutDocument> Get(int bookshelfId);
        BookCheckoutDocument Get(int bookshelfId, int bookId);
        BookCheckoutDocument Put(int bookshelfId, int bookId, int borrowerId, BookCheckoutDocument document);
    }
}