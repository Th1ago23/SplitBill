

using Domain.Interface.Database;
using Domain.Interface.Repository;
using Infrastructure.Repository;

namespace Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbConfig _context;

        public IUserRepository Users { get; }
        public IGroupRepository Groups { get; }
        public IExpenseRepository Expenses { get; }
        public UnitOfWork(DbConfig context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Groups = new GroupRepository(_context);
            Expenses = new ExpenseRepository(_context);
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
