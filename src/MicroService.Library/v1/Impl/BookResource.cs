using System.Collections.Generic;
using AutoMapper;
using MicroFx;
using MicroFx.Data;
using MicroFx.Data.Uow;
using MicroService.Library.Domain;
using MicroService.Library.v1.Contracts;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace MicroService.Library.v1.Impl
{
    public class BookResource : IBookResource
    {
        private readonly IRepository<Book> repository;

        public BookResource(IRepository<Book> repository)
        {
            this.repository = repository;
        }

       public BookDocument Get(int bookId)
        {
            var book = repository.Get(bookId);

            if (book == null)
                throw new ResourceNotFoundException("Book not Found");

            return Mapper.Map<BookDocument>(book);
        }

        [Transaction]
        public BookDocument Update(int bookId, BookDocument document)
        {
            var book = repository.Get(bookId);

            if (book == null)
                throw new ResourceNotFoundException("Book not Found");

            if (document == null) 
                throw new ValidationException("request was empty");

            book.Name = document.Name;
            book.Isbn = document.Isbn;
           repository.Update(book);
            
            return Mapper.Map<BookDocument>(book);
        }

        [Transaction]
        public void Delete(int bookId)
        {
            var book = repository.Get(bookId);

            if (book == null)
                throw new ResourceNotFoundException("Book not Found");

            repository.Delete(book);
        }

        [Transaction]
        public BookDocument Create(BookDocument document)
        {
            if (document == null) throw new ValidationException("request was empty");

            var book = new Book
            {
                Id = document.BookId,
                Name = document.Name,
                Isbn = document.Isbn,
            };

            repository.Save(book);

            return Mapper.Map<BookDocument>(book);
        }

        public IEnumerable<BookDocument> Get()
        {
            IEnumerable<Book> books;

            //if(!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(id) )
            // books = repository.Query().Where(x=> x.Name == name && x.Id == id);
            //else if (!string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(id))
            //    books = repository.Query().Where(x => x.Name == name );
            //else if (string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(id))
            //    books = repository.Query().Where(x => x.Id == id);
            //else
                books = repository.GetAll();

           return Mapper.Map<BookDocument[]>(books);
        }
    }
}
