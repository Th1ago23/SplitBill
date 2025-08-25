using Domain.Entity;

namespace Domain.Interface.Repository
{
    public interface IExpenseRepository
    {
        Task Add(Expense expense);
        void Update(Expense expense);
        Task Delete(int id);
        IQueryable<Expense> Get();

    }
}
