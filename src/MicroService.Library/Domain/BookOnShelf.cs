using System.Collections.Generic;

namespace MicroService.Library.Domain
{
    public class BookOnShelf
    {
        public BookOnShelf()
        {
            BookCheckouts = new List<BookCheckout>();
        }

        public int Id { get; set; }
        public int BookShelfId { get; set; }
        public int BookId { get; set; }
        public virtual ICollection<BookCheckout> BookCheckouts { get; set; }
    }
}