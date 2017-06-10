using System.Data.Entity.ModelConfiguration;

namespace MicroService.Library.Domain.Mappings
{
    public class BookCheckoutMap : EntityTypeConfiguration<BookCheckout>
    {
        public BookCheckoutMap()
        {
            ToTable("TBookCheckout");

            HasKey(t => t.Id);
            Property(t => t.Id).
                HasColumnName("BookCheckoutId");

            Property(t => t.BookOnShelfId).
                HasColumnName("BookOnShelfId")
                .IsRequired();
            
            Property(t => t.BorrowerId).
                HasColumnName("BorrowerId")
                .IsRequired();

            Property(t => t.Comment).
                HasColumnName("Comment")
                .HasMaxLength(2000);

            Property(t => t.CheckedOutAt).
                HasColumnName("CheckedOutAt")
                .IsRequired();

            Property(t => t.ReturnOn).
                HasColumnName("ReturnOn");
           

            this.HasRequired(t => t.BookOnShelf)
                .WithMany(t => t.BookCheckouts)
                .HasForeignKey(d => d.BookOnShelfId);

            this.HasRequired(t => t.Borrower)
                .WithMany(t => t.BookCheckouts)
                .HasForeignKey(d => d.BorrowerId);
        }
    }
}