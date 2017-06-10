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
    public class BookShelfResource : IBookShelfResource
    {
        private readonly IRepository<BookShelf> repository;

        public BookShelfResource(IRepository<BookShelf> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<BookShelfDocument> Get()
        {
            var bookshelves = repository.GetAll();
            return Mapper.Map<BookShelfDocument[]>(bookshelves);
        }
      
        public BookShelfDocument Get(int id)
        {
            var bookShelf = repository.Get(id);

            if (bookShelf == null)
                throw new ResourceNotFoundException("Book shelf not Found");

            return Mapper.Map<BookShelfDocument>(bookShelf);
        }

        [Transaction]
        public BookShelfDocument Update(int id, BookShelfDocument document)
        {
            var bookShelf = repository.Get(id);

            if (bookShelf == null)
                throw new ResourceNotFoundException("Book not Found");

            if (document == null)
                throw new ValidationException("request was empty");

            bookShelf.Name = document.Name;
           
            repository.Update(bookShelf);

            return Mapper.Map<BookShelfDocument>(bookShelf);
        }

        [Transaction]
        public void Delete(int id)
        {
            var bookShelf = repository.Get(id);

            if (bookShelf == null)
                throw new ResourceNotFoundException("Book shelf not Found");

            repository.Delete(bookShelf);
        }

       
        [Transaction]
        public BookShelfDocument Post(BookShelfDocument document)
        {
            if (document == null)
                throw new ValidationException("request was empty");

            var bookShelf = new BookShelf
            {
                Name = document.Name
            };

            repository.Save(bookShelf);

            return Mapper.Map<BookShelfDocument>(bookShelf);
        }
    }
}