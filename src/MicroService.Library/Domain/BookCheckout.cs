using System;

namespace MicroService.Library.Domain
{
    public class BookCheckout
    {
        public int Id { get; set; }
        public int BookOnShelfId { get; set; }
        public string Comment { get; set; }
        public DateTime CheckedOutAt { get; set; }
        public DateTime? ReturnOn { get; set; }
        public int BorrowerId { get; set; }
        public virtual BookOnShelf BookOnShelf { get; set; }
        public virtual Borrower Borrower { get; set; }
    }
}