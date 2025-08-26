
using Domain.DTO.Expense;
using Domain.Entity;
using Domain.Interface.Mapper.ExpenseMapper;
using Domain.Interface.Mapper.GroupMapper;
using Domain.Interface.Mapper.UserMapper;
using Domain.Interface.Utils;

namespace Application.Service.Mapper.ExpenseMapper
{
    public class ExpenseMP:IExpenseMP
    {
        private readonly IObjectValidator _validator;
        private readonly IUserMP _userMapper;
        private readonly IGroupMP _groupMP;

        public ExpenseMP (IGroupMP groupMP, IUserMP userMapper, IObjectValidator validator)
        {
            _groupMP = groupMP;
            _userMapper = userMapper;
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

        public ExpenseDetailDTO ToDetails(Expense expense)
        {
            _validator.Validate(expense);

            var userList = expense.Participants.Select(_userMapper.ToSummary).ToList();

            return new ExpenseDetailDTO(expense.Description, expense.Value, expense.Date, _userMapper.ToSummary(expense.Payer), userList, _groupMP.ToDTO(expense.Group));
           
        }

        public ExpenseResponseDTO ToResponse (Expense expense)
        {
            _validator.Validate(expense);

            return new ExpenseResponseDTO(expense.Description, expense.Value);

        }
    }
}