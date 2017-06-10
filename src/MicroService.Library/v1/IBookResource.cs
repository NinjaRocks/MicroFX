using System.Collections.Generic;
using MicroFx;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1
{
    public interface IBookResource: IResource
    {
        IEnumerable<BookDocument> Get();
        BookDocument Get(int bookId);
        BookDocument Update(int bookId, BookDocument document);
        void Delete(int bookId);
        BookDocument Create(BookDocument document);
    }
}