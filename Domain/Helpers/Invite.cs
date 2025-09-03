namespace Domain.Helpers;

public record Invite(Guid Id, int GroupId, int UserId, InviteExpirationTime Time)
{
}
