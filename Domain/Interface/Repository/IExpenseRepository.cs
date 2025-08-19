using Domain.Entity;

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
