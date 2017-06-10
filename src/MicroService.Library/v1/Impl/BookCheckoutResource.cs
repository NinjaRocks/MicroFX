using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MicroFx;
using MicroFx.Data;
using MicroService.Library.Domain;
using MicroService.Library.v1.Contracts;

namespace MicroService.Library.v1.Impl
{
    public class BookCheckoutResource : IBookCheckoutResource
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<BookShelf> bookShelfRepository;
        private readonly IRepository<BookOnShelf> bookInShelfRepository;
        private readonly IRepository<BookCheckout> checkoutRepository;
        private readonly IRepository<Borrower> borrowerRepository;

        public BookCheckoutResource(IRepository<Book> bookRepository
                                    , IRepository<BookShelf> bookShelfRepository
                                    , IRepository<BookOnShelf> bookInShelfRepository
                                    , IRepository<BookCheckout> checkoutRepository
                                    , IRepository<Borrower> borrowerRepository)
        {
            this.bookRepository = bookRepository;
            this.bookShelfRepository = bookShelfRepository;
            this.bookInShelfRepository = bookInShelfRepository;
            this.checkoutRepository = checkoutRepository;
            this.borrowerRepository = borrowerRepository;
        }


        public IEnumerable<BookCheckoutDocument> Get(int bookshelfId)
        {
            var bookShelf = bookShelfRepository.Get(bookshelfId);

            if (bookShelf == null)
                throw new ResourceNotFoundException("Book shelf not Found");
            
           
            var checkouts = checkoutRepository.Query()
                                    .Where(x => x.BookOnShelf.BookShelfId == bookshelfId && (x.ReturnOn == null || x.ReturnOn > DateTime.Today))
                                    .ToList();

            if (checkouts == null)
                throw new ResourceNotFoundException("Book is not checked out");

            var results = new List<BookCheckoutDocument>();

            results.AddRange(checkouts.Select(c =>
            {
                var result = Mapper.Map<BookCheckoutDocument>(c);
                result.Borrower = Mapper.Map<BorrowerDocument>(c.Borrower);
                return result;
            }));


            return results;
        }

        public BookCheckoutDocument Get(int bookshelfId, int bookId)
        {
            var bookShelf = bookShelfRepository.Get(bookshelfId);

            if (bookShelf == null)
                throw new ResourceNotFoundException("Book shelf not Found");

            var book = bookRepository.Get(bookId);
            if (book == null)
                throw new ResourceNotFoundException("Book not Found");

            var bookOnShelf = bookInShelfRepository.Query()
                .FirstOrDefault(x => x.BookShelfId == bookshelfId && x.BookId == bookId);


            if (bookOnShelf == null)
                throw new ResourceNotFoundException("Book is not in the self specified");

            var checkout = checkoutRepository.Query().FirstOrDefault(x => x.BookOnShelfId == bookOnShelf.Id &&  (x.ReturnOn == null || x.ReturnOn > DateTime.Today));

            if (checkout == null)
                throw new ResourceNotFoundException("Book is not checked out");
            
            
            return Map(checkout);
        }

        private static BookCheckoutDocument Map(BookCheckout checkout)
        {
            var result = Mapper.Map<BookCheckoutDocument>(checkout);
            result.Borrower = Mapper.Map<BorrowerDocument>(checkout.Borrower);
            return result;
        }


        public BookCheckoutDocument Put(int bookshelfId, int bookId, int borrowerId, BookCheckoutDocument document)
        {
            var bookShelf = bookShelfRepository.Get(bookshelfId);

            if (bookShelf == null)
                throw new ResourceNotFoundException("Book shelf not Found");

            var book = bookRepository.Get(bookId);
            if (book == null)
                throw new ResourceNotFoundException("Book not Found");

            var bookOnShelf = bookInShelfRepository.Query()
                .FirstOrDefault(x => x.BookShelfId == bookshelfId && x.BookId == bookId);

            if (bookOnShelf == null)
                throw new ResourceNotFoundException("Book is not in the self specified");

              var borrower = borrowerRepository.Get(borrowerId);
            if (borrower == null)
                throw new ResourceNotFoundException("Borrower not Found");


            var checkouts = checkoutRepository.Query().Where(x => x.BookOnShelfId == bookOnShelf.Id && (x.ReturnOn == null || x.ReturnOn > DateTime.Today))
                                                     .ToList();

            if (checkouts.Any(x=> x.BorrowerId != borrowerId))
                throw new ResourceNotFoundException("Book is already checked out to a diffrent borrower");

            checkouts.ForEach(c => checkoutRepository.Delete(c));

            var checkout = new BookCheckout
            {
                BorrowerId = borrowerId,
                CheckedOutAt = document.CheckedOutAt,
                BookOnShelfId = bookOnShelf.Id,
                BookOnShelf = bookOnShelf,
                Comment = document.Comment,
                ReturnOn = document.ReturnOn,
                Borrower = borrower
            };

            checkoutRepository.Save(checkout);

            return Map(checkout);
        }
    }
}