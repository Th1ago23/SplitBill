
using Domain.Interface.Repository;

namespace Domain.Interface.Database
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IGroupRepository Groups { get; }
        IExpenseRepository Expenses { get; }

        Task<int> CommitAsync();
        void Dispose();
    }
}
