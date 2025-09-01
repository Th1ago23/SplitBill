
using Application.DTO.Expense;
using Application.Interface.Mapper.ExpenseMapper;
using Domain.Entity;
using Domain.Interface.Utils;

namespace Application.Service.Mapper.ExpenseMapper
{
    public class ExpenseMP : IExpenseMP
    {
        private readonly IObjectValidator _validator;

        public ExpenseMP(IObjectValidator validator)
        {
            _validator = validator;
        }
        public Expense ToEntity(ExpenseCreateDTO dto)
        {
            _validator.Validate(dto);

            return new Expense
            {
                Date = dto.Date,
                Description = dto.description,
                Value = dto.value,
                PaidByUserId = dto.PaidByUserId,
            };
        }

        public ExpenseResponseDTO ToResponse(Expense expense)
        {
            _validator.Validate(expense);

            return new ExpenseResponseDTO(expense.Id, expense.Description, expense.Value);
        }
    }
}