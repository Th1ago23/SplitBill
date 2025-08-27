using Domain.DTO.Expense;
using Domain.Entity;

namespace Domain.Interface.Mapper.ExpenseMapper
{
    public interface IExpenseMP
    {
        public Expense ToEntity(ExpenseCreateDTO dto);
        public ExpenseResponseDTO ToResponse(Expense expense);

    }
}
