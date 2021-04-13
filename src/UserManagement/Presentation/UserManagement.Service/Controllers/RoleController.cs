using System.Threading.Tasks;
using Framework.Buses;
using Framework.Controller.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Query.Model.AdminRoleQuery;
using ServiceContract.Command.UserRoleCommands;

namespace UserManagement.Service.Controllers
{
    [Route("api/v1/[controller]")]

    public class RoleController:BaseController
    {
        private readonly IEventBus _eventBus;

        public RoleController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> AddRole(AddRoleCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> UpdateRole(EditRoleCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var command = new DeleteRoleCommandRequest()
            {
                Id = id
            };
            await _eventBus.Issue(command);
            return Ok();
        }
        [HttpPost("adduser-role")]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> AddRoleToUser(AddRoleToUserCommandRequest command)
        {
            await _eventBus.Issue(command);
            return Ok();
        }

        [HttpGet("admin-role/{id}")]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> GetById(int id)
        {
            var data = await _eventBus.IssueQuery(new GetAdminRoleByIdQueryRequest
            {
                Id = id
            });
            return Ok(data);
        }
        [HttpGet()]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult> GetAll()
        {
            var data = await _eventBus.IssueQuery(new GetAllAdminRolesQueryRequest());
            return Ok(data);
        }
        
    }
}