using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MicroFx;
using MicroFx.Data;
using MicroFx.Data.Uow;
using MicroService.Library.Domain;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1.Impl
{
    public class BookOnShelfResource : IBookOnShelfResource
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<BookShelf> bookShelfRepository;
        private readonly IRepository<BookOnShelf> repository;

        public BookOnShelfResource(IRepository<BookOnShelf> repository
                                    , IRepository<Book> bookRepository
                                    , IRepository<BookShelf> bookShelfRepository)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.bookShelfRepository = bookShelfRepository;
        }

        public IEnumerable<BookOnShelfDocument> Get(int bookshelfId)
        {
            var result = new List<BookOnShelfDocument>();
            var bookShelf = bookShelfRepository.Get(bookshelfId);

            if (bookShelf == null)
                throw new ResourceNotFoundException("Book shelf not Found");

            var booksOnShelves = repository.Query().Where(x => x.BookShelfId == bookshelfId);
            
            // could map as relationships and queried  
            foreach (var bookOnShelf in booksOnShelves)
            {
                var book = bookRepository.Get(bookOnShelf.BookId);
                result.Add(Map(bookOnShelf, book, bookShelf));
            }

            return result;
        }

        public BookOnShelfDocument Get(int bookshelfId, int bookId)
        {
            var bookShelf = bookShelfRepository.Get(bookshelfId);

            if (bookShelf == null)
                throw new ResourceNotFoundException("Book shelf not Found");

            var book = bookRepository.Get(bookId);

            if (book == null)
                throw new ResourceNotFoundException("Book not Found");

            var bookInShelf = repository.Query().FirstOrDefault(x => x.BookShelfId == bookshelfId && x.BookId == bookId);

            if (bookInShelf == null)
                throw new ResourceNotFoundException("Book is not found in shelf");

            var document = Map(bookInShelf, book, bookShelf);

            return document;
        }

        
        [Transaction]
        public void Delete(int bookshelfId, int bookId)
        {
            var bookShelf = bookShelfRepository.Get(bookshelfId);

            if (bookShelf == null)
                throw new ResourceNotFoundException("Book shelf not Found");

            var book = bookRepository.Get(bookId);

            if (book == null)
                throw new ResourceNotFoundException("Book not Found");


            var bookInShelf = repository.Query().FirstOrDefault(x => x.BookShelfId == bookshelfId && x.BookId == bookId);

            if (bookInShelf == null)
                throw new ResourceNotFoundException("Book is not found in shelf");

            repository.Delete(bookInShelf);
        }


        [Transaction]
        public BookOnShelfDocument Post(int bookshelfId, int bookId)
        {
            var bookShelf = bookShelfRepository.Get(bookshelfId);

            if (bookShelf == null)
                throw new ResourceNotFoundException("Book shelf not Found");

            var book = bookRepository.Get(bookId);

            if (book == null)
                throw new ResourceNotFoundException("Book not Found");


            var bookInShelf = new BookOnShelf
            {
                BookId = bookId,
                BookShelfId = bookshelfId
            };

            repository.Save(bookInShelf);

            var document = Map(bookInShelf, book, bookShelf);

            return document;
        }

        private static BookOnShelfDocument Map(BookOnShelf bookOnShelf, Book book, BookShelf bookShelf)
        {
            var document = Mapper.Map<BookOnShelfDocument>(bookOnShelf);
            document.Book = Mapper.Map<BookDocument>(book);
            document.BookShelf = Mapper.Map<BookShelfDocument>(bookShelf);
            return document;
        }
    }
}