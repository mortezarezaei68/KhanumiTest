using System.Threading.Tasks;
using Framework.Buses;
using Framework.Buses.RabbitMQBus;
using Framework.Controller.Extensions;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.CommandHandler;
using TicketManagement.Commands;
using TicketManagement.Query.Models.TicketQueryModel;

namespace TicketManagement.Service.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/v1/[controller]")]
    public class TicketController:BaseController
    {
        private readonly IEventBus _eventBus;
        private readonly IRabbitMqBus _rabbitMqBus;

        public TicketController(IEventBus eventBus, IRabbitMqBus rabbitMqBus)
        {
            _eventBus = eventBus;
            _rabbitMqBus = rabbitMqBus;
        }
        [HttpPost]
        public async Task<ActionResult> AddTicket(AddTicketCommandRequest command)
        {
            var response=await _eventBus.Issue(command);
            if (response.IsSuccess)
            {
                _rabbitMqBus.Publish(new TicketSmsEvent("Test",response.Data.TrackingCode));
            }
            return Ok(response);
        }
        [HttpPut("update-ticket-state")]
        public async Task<ActionResult> UpdateTicket(UpdateTicketStateCommandRequest command)
        {
            var response=await _eventBus.Issue(command);
            return Ok(response);
        }
        [HttpPost("Add-Answer-Ticket")]
        public async Task<ActionResult> AddAnswerTicket(AddAnswerTicketCommandRequest command)
        {
            var response=await _eventBus.Issue(command);
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllTickets()
        {
            var response=await _eventBus.IssueQuery(new GetAllTicketsQueryRequest());
            return Ok(response);
        }
    }
}