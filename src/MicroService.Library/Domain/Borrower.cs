using System.Collections.Generic;

namespace MicroService.Library.Domain
{
    public class Borrower
    {
        public Borrower()
        {
            BookCheckouts = new List<BookCheckout>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BookCheckout> BookCheckouts { get; set; }
    }
}