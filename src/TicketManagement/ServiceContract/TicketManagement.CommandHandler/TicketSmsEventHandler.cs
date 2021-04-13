using System.Text;
using System.Threading.Tasks;
using Framework.Commands;
using Microsoft.Extensions.Logging;
using TicketManagement.Commands;

namespace TicketManagement.CommandHandler
{
    public class TicketSmsEventHandler:IEventHandler<TicketSmsEvent>
    {
        private readonly ILogger<TicketSmsEventHandler> _logger;

        public TicketSmsEventHandler(ILogger<TicketSmsEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TicketSmsEvent @event)
        {
            var stringBuilder = new StringBuilder($"Handling rabbit integr" + $"ation event: {@event.TrackingCode}");
            _logger.LogInformation(stringBuilder.ToString());
            return Task.CompletedTask;
        }
    }
}