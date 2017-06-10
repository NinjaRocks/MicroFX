using System.Data.Entity.ModelConfiguration;

namespace MicroService.Library.Domain.Mappings
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            ToTable("TBook");

            HasKey(t => t.Id);
            Property(t => t.Id).
                HasColumnName("BookId");
          

            Property(t => t.Name).
                HasColumnName("Name").
                HasMaxLength(255).  
                IsRequired();

            Property(t => t.Isbn).
                HasColumnName("ISBN").
                HasMaxLength(255).
                IsRequired();
        }
    }
}