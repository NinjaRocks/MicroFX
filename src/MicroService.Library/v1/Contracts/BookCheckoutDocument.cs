using System;

namespace MicroService.Library.v1.Contracts
{
    public class BookCheckoutDocument
    {
        public int BookCheckoutId { get; set; }
        public int BookOnShelfId { get; set; }
        public int BookShelfId { get; set; }
        public int BookId { get; set; }
        public BorrowerDocument Borrower { get; set; }
        public string Comment { get; set; }
        public DateTime CheckedOutAt { get; set; }
        public DateTime? ReturnOn { get; set; }
    }
}