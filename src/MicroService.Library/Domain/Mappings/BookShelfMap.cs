using System.Data.Entity.ModelConfiguration;

namespace MicroService.Library.Domain.Mappings
{
    public class BookShelfMap : EntityTypeConfiguration<BookShelf>
    {
        public BookShelfMap()
        {
            ToTable("TBookShelf");

            HasKey(t => t.Id);
            Property(t => t.Id).
                HasColumnName("BookShelfId");

            Property(t => t.Name).
                HasColumnName("Name").
                HasMaxLength(255).
                IsRequired();
        }
    }
}