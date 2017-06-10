using System.Data.Entity.ModelConfiguration;

namespace MicroService.Library.Domain.Mappings
{
    public class BorrowerMap : EntityTypeConfiguration<Borrower>
    {
        public BorrowerMap()
        {
            ToTable("TBorrower");

            HasKey(t => t.Id);
            Property(t => t.Id).
                HasColumnName("BorrowerId");

            Property(t => t.Name).
                HasColumnName("Name").
                HasMaxLength(255).
                IsRequired();
        }
    }
}