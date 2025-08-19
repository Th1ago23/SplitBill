using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Expense
{
    public record ExpenseCreateDTO([Required] double value, [Required] string description, DateTime Date, int? PaidByUserId, List<int?> ParticipantsId)
    { }
}
