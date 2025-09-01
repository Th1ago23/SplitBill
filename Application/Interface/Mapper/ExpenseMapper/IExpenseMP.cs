using Application.DTO.Expense;
using Domain.Entity;

namespace Application.Interface.Mapper.ExpenseMapper
{
    public interface IExpenseMP
    {
        public Expense ToEntity(ExpenseCreateDTO dto);
        public ExpenseResponseDTO ToResponse(Expense expense);

    }
}
