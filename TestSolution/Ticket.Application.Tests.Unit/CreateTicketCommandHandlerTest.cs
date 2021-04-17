using System;
using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using FluentValidation.TestHelper;
using MediatR;
using NSubstitute;
using TicketManagement.CommandHandler;
using TicketManagement.Commands;
using TicketManagement.Domain;
using Xunit;
using TicketDomain = TicketManagement.Domain.Ticket;

namespace Ticket.Application.Tests.Service
{
    public class CreateTicketCommandHandlerTest
    {
        private readonly AddTicketCommandRequestValidator _validator;

        public CreateTicketCommandHandlerTest()
        {
            _validator = new AddTicketCommandRequestValidator();
        }

        [Fact]
        public void should_create_ticket()
        {
            var command = new AddTicketCommandRequest
            {
                Description = "Test Description",
                Title = "Test title"
            };
            var repository = Substitute.For<ITicketRepository>();
            var handler = new AddTicketCommandHandler(repository);
            var mockPipelineBehaviourDelegate = Substitute.For<RequestHandlerDelegate<AddTicketCommandResponse>>();
            handler.Handle(command, default(CancellationToken), mockPipelineBehaviourDelegate).Wait();
            repository.Received(Times.Once()).Add(Verify.That<TicketDomain>(ticket =>
            {
                ticket.Description.Should().Be(command.Description);
                ticket.Title.Should().Be(command.Title);
            }));
            
        }
        
        [Theory]
        [InlineData("", null)]
        [InlineData("name", null)]
        [InlineData(null,"name")]
        public void Should_have_error_when_properties_are_not_filled(string description,string title)
        {
            var model = new AddTicketCommandRequest() {Description = description,Title = title};
            var result = _validator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }
    }
}