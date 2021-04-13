using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManagement.Domain;

namespace TicketManagement.Persistance.EF.EntityConfiguration
{
    public class TicketEntityConfiguration:IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasMany(a => a.AnswerTickets);
        }
    }
}