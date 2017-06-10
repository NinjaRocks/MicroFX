using System.Data.Entity.ModelConfiguration;

namespace MicroService.Library.Domain.Mappings
{
    public class BookOnShelfMap : EntityTypeConfiguration<BookOnShelf>
    {
        public BookOnShelfMap()
        {
            ToTable("TBookOnShelf");

            HasKey(t => t.Id);
            Property(t => t.Id)
                .HasColumnName("BookOnShelfId");

            Property(t => t.BookId)
                .HasColumnName("BookId")
                .IsRequired();

            Property(t => t.BookShelfId).
                HasColumnName("BookShelfId")
                .IsRequired();
        }
    }
}