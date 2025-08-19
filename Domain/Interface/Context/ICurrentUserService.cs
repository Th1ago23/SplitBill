namespace Domain.Interface.Context
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        string? UserEmail { get; }
    }
}
