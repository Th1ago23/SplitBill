using Application.DTO.Group;
using Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SplitBill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _service;

        public GroupController(IGroupService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("GroupCreate")]
        public async Task<IActionResult> CreateGroup(GroupCreateDTO request)
        {
            await _service.CreateGroup(request);

            if (request == null) BadRequest("Não foi possível criar um grupo.");

            return Created("api/groups", new
            {
                group = request,
                message = "Grupo criado com sucesso!!"
            });

        }

        [Authorize]
        [HttpGet("GetAllGroupsWithMembers")]
        public async Task<IActionResult> GetGroupsWithMembers()
        {
            var gp = await _service.GetAllGroupsWithMembersInContext();
            return Ok(gp);
        }

        [Authorize]
        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupById (int groupId)
        {
            try
            {
                var gp = await _service.GetGroupById(groupId);
                return Ok(gp);
            } catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            } catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [Authorize]
        [HttpPost("{groupId}/AddMember/{userEmail}", Name = "Add Member")]
        public async Task<IActionResult> AddMember(int groupId, string userEmail)
        {
            await _service.AddMember(groupId, userEmail);
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
        [HttpGet("GetGroupName", Name = "Get Group Name")]
        public async Task<IActionResult> GetGroupName(int gpId)
        {
            var gpName = await _service.GetGroupName(gpId);

            return Ok(gpName);
        }

        [Authorize]
        [HttpDelete("{groupId}/RemoveMember/{userToRemoveId}", Name = "Remove Member")]
        public async Task<IActionResult> RemoveMember(int groupId, int userToRemoveId)
        {
            await _service.RemoveMember(groupId, userToRemoveId);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("DeleteGroup/{groupId}", Name = "Delete Group")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            await _service.DeleteGroup(groupId);

            return NoContent();
        }

        [Authorize]
        [HttpPatch("{groupId}/RenameGroup")]
        public async Task<IActionResult> RenameGroup(string newName, int groupId)
        {
            var name = await _service.RenameGroup(newName, groupId);

            return Ok(name);
        }
    }
}
