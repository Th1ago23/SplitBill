using Application.DTO.Expense;
using Application.Interface.Mapper.ExpenseMapper;
using Application.Interface.Mapper.GroupMapper;
using Application.Interface.Mapper.UserMapper;
using Domain.Entity;


namespace Application.Service.Mapper
{
    public class GroupGeneralMapper
    {
        private readonly IExpenseMP _emp;
        private readonly IUserMP _usmp;
        private readonly IGroupMP _gmp;

        public GroupGeneralMapper(IGroupMP gmp, IExpenseMP emp, IUserMP usmp)
        {
            _gmp = gmp;
            _emp = emp;
            _usmp = usmp;
        }
        public ExpenseDetailDTO ToDetail (Expense ex)
        {
            if (ex == null) throw new NullReferenceException();
            return new ExpenseDetailDTO(ex.Id,ex.Description, ex.Value, ex.Date, _usmp.ToSummary(ex.Payer), ex.Participants.Select(_usmp.ToSummary).ToList(), _gmp.ToDTO(ex.Group));
        }

    }
}
