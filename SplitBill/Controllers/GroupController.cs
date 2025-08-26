using Domain.DTO.Group;
using Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SplitBill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController:ControllerBase
    {
        private readonly IGroupService _service;

        public GroupController(IGroupService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("GroupCreate")]       
        public IActionResult CreateGroup (GroupCreateDTO request)
        {
            _service.CreateGroup(request);

            if (request == null) BadRequest("Não foi possível criar um grupo.");

            return CreatedAtAction("Create Group", new
            {
                group = request,
                message = "Grupo criado com sucesso!"
            });
        
        }

        [Authorize]
        [HttpPost("{groupId}/AddMember/{userEmail}", Name ="Add Member")]
        public IActionResult AddMember (int groupId, string userEmail)
        {         
            _service.AddMember(groupId, userEmail);
            var gpName = _service.GetGroupName(groupId);


            return CreatedAtAction("Add member",
                new
                {
                    groupName = gpName,
                    usEmail = userEmail,
                    message = "Usuário adicionado ao grupo com sucesso!"
                });                      
        }

        [Authorize]
        [HttpGet("GetGroupName", Name="Get Group Name")]
        public IActionResult GetGroupName(int gpId)
        {
            var gpName = _service.GetGroupName(gpId);

            return Ok(gpName);
        }

        [Authorize]
        [HttpDelete("{groupId}/RemoveMember/{userToRemoveId}", Name ="Remove Member")]
        public IActionResult RemoveMember(int groupId, int userToRemoveId)
        {
            _service.RemoveMember(groupId, userToRemoveId);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("DeleteGroup/{groupId}", Name ="Delete Group")]
        public IActionResult DeleteGroup(int groupId)
        {
            _service.DeleteGroup(groupId);

            return NoContent();
        }

        [Authorize]
        [HttpPatch("{groupId}/RenameGroup")]
        public IActionResult RenameGroup(string newName, int groupId)
        {
            var name = _service.RenameGroup(newName, groupId);

            return Ok(name);
        }
    }
}
