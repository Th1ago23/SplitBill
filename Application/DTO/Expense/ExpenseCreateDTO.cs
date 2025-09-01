using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Expense
{
    public record ExpenseCreateDTO([Required] decimal value, [Required] string description, DateTime Date, int? PaidByUserId, ICollection<int> ParticipantsIds)
    { }
}
