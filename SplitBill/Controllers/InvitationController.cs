using Application.DTO.Invitation;
using Application.Interface.Utils;
using Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SplitBill.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvitationController:ControllerBase
{
    private readonly IInvitationService _invitation;

    public InvitationController(IInvitationService invitation)
    {
        _invitation = invitation;
    }

    [Authorize]
    [HttpPost("Group/{groupId}/CreateInvite")]
    public async Task<IActionResult> Invite (int groupId, InviteExpirationTime time)
    {
        var token = _invitation.CreateInvite(groupId, time);

        return Ok(new
        {
            Token = token,
            Message = "Convite criado com sucesso"
        });
    }

    [HttpPost("groups/join")]
    public async Task<IActionResult> JoinGroupInvited([FromBody] JoinGroupRequest request)
    {
        await _invitation.JoinGroup(request.Token);
        return Ok(new { Message = "Você entrou no grupo" });
    }
}
