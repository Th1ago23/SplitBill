using Domain.Entity;

namespace Domain.Interface.Repository
{
    public interface IUserRepository
    {
        public Task Add(User user);
        public void Update(User user);
        public Task Delete(int id);
        public IQueryable<User> Find();

    }
}
