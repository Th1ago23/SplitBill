namespace Domain.Helpers;

public static class InviteExtensions
{
    public static DateTime ToTime(this InviteExpirationTime option)
    {
        return option switch
        {
            InviteExpirationTime.ThirtyMinutes => DateTime.UtcNow.AddMinutes(30),
            InviteExpirationTime.OneHour => DateTime.UtcNow.AddHours(1),
            InviteExpirationTime.OneDay => DateTime.UtcNow.AddDays(1),
            _ => throw new ArgumentOutOfRangeException(nameof(option), "Opção inválida")
        };
    }
}
