using System.Linq;
using FluentAssertions;
using Framework.Common;
using Xunit;
using DomainTicket = TicketManagement.Domain.Ticket;

namespace Ticket.Domain.Tests.Unit
{
    public class TicketTests
    {
        private static DomainTicket GetTicket()
        {
            var ticket = new DomainTicket("Test Ticket Domain", "Test Ticket",CodeGenerator.CreateCode());
            const string answer = "Test answer";

            ticket.AddAnswer(answer);
            ticket.AnswerTickets.First().SetId(1);

            return ticket;
        }
        [Fact]
        public void Should_create_a_ticket()
        {
            const string description = "Test Ticket Domain";
            const string title = "Test Ticket";
            var trackingCode =CodeGenerator.CreateCode();
            
            var ticket =
                new DomainTicket(description,title,trackingCode );
            ticket.Description.Should().Be(description);
            ticket.Title.Should().Be(title);
            ticket.TrackingCode.Should().Be(trackingCode);
        }   
        
        [Fact]
        public void Should_create_a_ticket_answer()
        {
            const string description = "Test Ticket Domain";
            const string title = "Test Ticket";
            var trackingCode =CodeGenerator.CreateCode();
            const string answer = "answer";
            
            var ticket =
                new DomainTicket(description,title,trackingCode);
            ticket.AddAnswer(answer);
            ticket.AnswerTickets.Should().HaveCount(1);
            ticket.AnswerTickets.Should().SatisfyRespectively(first =>
            {
                first.Answer.Should().Be(answer);
            });
        }
        [Fact]
        public void Should_update_a_ticket()
        {
            var ticketValue = GetTicket();
            const string updateAnswerValue = "Update Answer";
            ticketValue.UpdateAnswer(updateAnswerValue,1);
            ticketValue.AnswerTickets.Should().SatisfyRespectively(first =>
            {
                first.Answer.Should().Be(updateAnswerValue);
            });
        }
    }
}