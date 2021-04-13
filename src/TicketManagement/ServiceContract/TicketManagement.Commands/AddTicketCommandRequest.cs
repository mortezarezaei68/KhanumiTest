using System;
using MediatR;

namespace TicketManagement.Commands
{
    public class AddTicketCommandRequest:IRequest<AddTicketCommandResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}