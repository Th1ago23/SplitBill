using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Repository
{
    public interface IExpenseRepository
    {
        Task Add(Expense expense);
        Task Update(Expense expense);
        Task Delete(int id);
        Task<Expense> Find(int id);
        
    }
}
