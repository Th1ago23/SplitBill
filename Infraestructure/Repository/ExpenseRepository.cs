#pragma warning disable IDE0290
using Domain.Entity;
using Domain.Interface.Repository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly DbConfig _context;

        public ExpenseRepository(DbConfig context)
        {
            _context = context;
        }

        public async Task Add(Expense ex)
        {
            await _context
                        .Expenses
                        .AddAsync(ex);


        }

        public void Update(Expense ex)
        {
            _context
                .Expenses
                .Update(ex);

        }

        public async Task Delete(int id)
        {
            var expense = await _context
                                     .Expenses
                                     .FirstOrDefaultAsync(i => i.Id == id)
                                     ?? throw new NullReferenceException($"Não foram encontrados nenhuma despesa com o ID {id}.");
            _context.Remove(expense);
        }

        public IQueryable<Expense> Find()
        {
            return _context
                        .Expenses
                        .AsQueryable();
        }
    }
}
