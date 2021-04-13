using Framework.Context;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Domain;
using TicketManagement.Persistance.EF.EntityConfiguration;

namespace TicketManagement.Persistance.EF.Context
{
    public class TicketManagementDbContext:CoreDbContext
    {
        public TicketManagementDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketEntityConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<AnswerTicket> AnswerTickets { get; set; }
    }
}