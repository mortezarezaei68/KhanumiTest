using System.Threading.Tasks;
using Framework.Buses;
using Framework.Controller.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Query.Model.AdminUserQuery;
using ServiceContract.Command.AdminUserCommands;
using ServiceContract.Command.UserTokenCommands;

namespace UserManagement.Service.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController:BaseController
    {
        private readonly IEventBus _eventBus;

        public UserController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        [HttpPost]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> AddAdminUser(AddAdminUserCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> UpdateAdminUser(EditAdminUserCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }
        [HttpPut("update-user-password")]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> UpdateAdminUserPassword(UpdateAdminUserPasswordRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "superuser,admin")]
        public async Task<ActionResult> UpdateAdminUser(string id)
        {
            var command = new DeleteAdminUserCommandRequest
            {
                Id = id
            };
            await _eventBus.Issue(command);
            return Ok();
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult> GetToken(LoginAdminUserCommandRequest command)
        {
            var data=await _eventBus.Issue(command);
            return Ok(data);
        }
        [HttpPost("SignOutUser")]
        [Authorize]
        public async Task<ActionResult> SignOut()
        {
            var command = new SignOutAdminUserCommandRequest();
            var data=await _eventBus.Issue(command);
            return Ok(data);
        }

        [HttpGet("admin-user/{id}")]
        [Authorize(Roles = "superuser,admin")]
        public async Task<ActionResult> GetAdminUserById(int id)
        {
            var data = await _eventBus.IssueQuery(new GetAdminUserQueryRequest
            {
                UserId = id
            });
            return Ok(data);
        }
        
        [HttpGet]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> GetAllAdminUser()
        {
            var data = await _eventBus.IssueQuery(new GetAllAdminUserQueryRequest());
            return Ok(data);
        }
        [HttpGet("current-admin-user")]
        [Authorize]
        public async Task<ActionResult> GetCurrentAdminUser()
        {
            var data = await _eventBus.IssueQuery(new GetCurrentAdminUserQueryRequest());
            return Ok(data);
        }
        [HttpPost("get-refresh-token")]
        public async Task<ActionResult> GetRefreshToken(ExtendAccessTokenCommandRequest command)
        {
            var data=await _eventBus.Issue(command);
            return Ok(data);
        }
    }
}