#pragma warning disable IDE0290
using Domain.Entity;
using Domain.Interface.Repository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConfig _context;

        public UserRepository(DbConfig context)
        {
            _context = context;
        }


        public async Task Add(User user)
        {
            await _context
                    .Users
                    .AddAsync(user);

        }

        public void Update(User user)
        {
            _context
                .Users
                .Update(user);

        }

        public async Task Delete(int id)
        {
            var user = await _context
                                .Users
                                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null) throw new NullReferenceException(nameof(user));

            _context.Remove(user);
        }

        public IQueryable<User> Find()
        {
            return _context.Users.AsQueryable();
        }


    }
}
