
using Application.Interface.Utils;
using Domain.Helpers;
using Domain.Interface.Context;
using Domain.Interface.Database;
using Domain.Interface.Repository;
using Domain.Interface.Token;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Service;

public class InvitationService:IInvitationService
{
    private readonly ITokenService _token;
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _work;

    public InvitationService(IUnitOfWork work, ICurrentUserService currentUser, IUserRepository userRepository, IGroupRepository groupRepository, ITokenService token)
    {
        _work = work;
        _currentUser = currentUser;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
        _token = token;
    }

    public async Task<string> CreateInvite (int groupId, InviteExpirationTime time )
    {
        var group = await _groupRepository.Find().Include(i=> i.Users).FirstOrDefaultAsync(i => i.Id == groupId) ?? throw new NullReferenceException("Grupo não encontrado");
        var user = await _userRepository.Find().FirstOrDefaultAsync(i => i.Id == _currentUser.UserId) ?? throw new NullReferenceException("Usuário não encontrado");

        if (!group.IsPublic && group.LeaderId != user.Id) throw new UnauthorizedAccessException("Sem permissão para convidar outros usuários");

        var invite = new Invite(Guid.NewGuid(), group.Id, user.Id,time);

        var token = _token.GenerateInviteToken(invite);

        return token;
    }
    public async Task<bool> JoinGroup (string token)
    {
        var user = await _userRepository.Find().FirstOrDefaultAsync(i => i.Id == _currentUser.UserId) ?? throw new UnauthorizedAccessException("Para entrar em um grupo, crie uma conta");

        ClaimsPrincipal principal = _token.ValidateToken(token);

        var groupId = int.Parse(principal.FindFirst("GroupId")?.Value
                ?? throw new Exception("Claim GroupId não encontrada"));
        var inviteUserId = int.Parse(principal.FindFirst("UserId")?.Value
                        ?? throw new Exception("Claim UserId não encontrada"));
        var inviteId = Guid.Parse(principal.FindFirst("InviteId")?.Value
                        ?? throw new Exception("Claim InviteId não encontrada"));

        var group = await _groupRepository.Find().Include(i => i.Users).FirstOrDefaultAsync(i => i.Id == groupId) ?? throw new NullReferenceException("Grupo não existe");

        if (!group.IsPublic && group.LeaderId != inviteUserId)
            throw new UnauthorizedAccessException("Token de invite inválido para grupo privado");

        bool alreadyInGroup = await _groupRepository.Find()
            .Where(g => g.Id == groupId && g.Users.Any(u => u.Id == user.Id))
            .AnyAsync();

        if (alreadyInGroup)
            throw new ArgumentException("Usuário já está no grupo");

        group.Users.Add(user);

        await _work.CommitAsync();

        return true;
    }
}
