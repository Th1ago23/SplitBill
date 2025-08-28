using Domain.DTO.Expense;
using Domain.Entity;
using Domain.Interface.Mapper.ExpenseMapper;
using Domain.Interface.Mapper.GroupMapper;
using Domain.Interface.Mapper.UserMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new ExpenseDetailDTO(ex.Description, ex.Value, ex.Date, _usmp.ToSummary(ex.Payer), ex.Participants.Select(_usmp.ToSummary).ToList(), _gmp.ToDTO(ex.Group));
        }

    }
}
