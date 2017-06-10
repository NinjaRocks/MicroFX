namespace MicroService.Library.v1.Contracts
{
    public class BookOnShelfDocument
    {
        public int BookOnShelfId { get; set; }
        public BookShelfDocument BookShelf { get; set; }
        public BookDocument Book { get; set; }
    }
}