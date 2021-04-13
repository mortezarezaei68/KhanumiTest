using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domains;
using UserManagement.Domains.Customer;

namespace UserManagement.Persistance.DomainConfigurations
{
    public class GenderEntityConfiguration:IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasMany(a => a.Users).WithOne(a => a.Gender).HasForeignKey(a => a.GenderId);
        }
    }
}