using Domain.DTO.Expense;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Mapper.ExpenseMapper
{
    public interface IExpenseMP
    {
        public Expense ToEntity(ExpenseCreateDTO dto);
        public ExpenseDetailDTO ToDetails (Expense expense);
        public ExpenseResponseDTO ToResponse(Expense expense);

    }
}
