using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domains;
using UserManagement.Domains.Customer;

namespace UserManagement.Persistance.DomainConfigurations
{
    public class UserEntityConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.HasMany(a => a.PersistGrants).WithOne(a=>a.User);
            
        }
    }
}