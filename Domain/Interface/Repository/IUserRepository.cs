using Domain.Entity;

namespace Domain.Interface.Repository
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task Update(User user);
        Task Delete(int id);
        Task<User> Find (int id);
        Task<IEnumerable<User>> FindAll();
        //Task<User> FindInContext();

    }
}
