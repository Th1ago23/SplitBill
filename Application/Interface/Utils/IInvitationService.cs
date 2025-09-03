using Domain.Helpers;


namespace Application.Interface.Utils;

public interface IInvitationService
{
    public Task<string> CreateInvite(int groupId, InviteExpirationTime time);
    public Task<bool> JoinGroup(string token);


}
