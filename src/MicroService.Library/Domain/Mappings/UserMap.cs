using System.Data.Entity.ModelConfiguration;

namespace MicroService.Library.Domain.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("TUser");

            HasKey(t => t.Id);
            Property(t => t.Id).
                HasColumnName("UserId");

            Property(t => t.Email).
                HasColumnName("Email").
                HasMaxLength(255).
                IsRequired();

            Property(t => t.Password).
                HasColumnName("Password").
                HasMaxLength(255).
                IsRequired();
        }
    }
}