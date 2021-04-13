using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domains;

namespace UserManagement.Persistance.DomainConfigurations
{
    public class RoleEntityConfiguration:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }
}