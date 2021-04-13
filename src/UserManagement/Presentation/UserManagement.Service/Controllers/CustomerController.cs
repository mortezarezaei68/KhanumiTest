using System.Threading.Tasks;
using Framework.Buses;
using Framework.Controller.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Query.Model.CustomerUserQuery;
using ServiceContract.Command.CustomerUserCommands;

namespace UserManagement.Service.Controllers
{
    [Route("api/v1/[controller]")]
    public class CustomerController:BaseController
    {
        private readonly IEventBus _eventBus;

        public CustomerController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        [HttpPost]
        public async Task<ActionResult> AddCustomerUser(AddCustomerCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateCustomerUser(EditCustomerCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }

        [HttpPut("update-customer-password")]
        [Authorize]
        public async Task<ActionResult> UpdateCustomerPassword(UpdateCustomerPasswordCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }

        [HttpPost("LoginCustomer")]
        public async Task<ActionResult> GetToken(LoginCustomerUserCommandRequest command)
        {
            var data=await _eventBus.Issue(command);
            return Ok(data);
        }

        [HttpGet("customerByTokenId")]
        [Authorize]
        public async Task<ActionResult> GetCustomerById()
        {
            var data = await _eventBus.IssueQuery(new CustomerByTokenIdQueryRequest());
            return Ok(data);
        }

        [HttpGet]
        [Authorize(Roles = "superuser,admin")]
        public async Task<ActionResult> GetCustomers()
        {
            var data=await _eventBus.IssueQuery(new GetAllCustomerQueryRequest());
            return Ok(data);
        }
    }
}