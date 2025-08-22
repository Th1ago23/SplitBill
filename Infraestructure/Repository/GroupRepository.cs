#pragma warning disable IDE0290
using Domain.Entity;
using Domain.Interface.Repository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
    public class GroupRepository:IGroupRepository
    {
        private readonly DbConfig _context;

        public GroupRepository(DbConfig context)
        {
            _context = context;
        }

        public async Task<bool> Create(Group gp)
        {
            if (gp == null) return false;

            _context.Groups.Add(gp);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task Rename (string newName, Group gp)
        {
            var gp1 = await _context
                                .Groups
                                .FirstOrDefaultAsync(i => i.Id == gp.Id);

            if (gp1 == null) throw new NullReferenceException(nameof(gp1));

            gp1.Name = newName;

            await _context
                        .SaveChangesAsync();
        }

        public async Task Update (Group gp)
        {
            _context
                .Groups
                .Update(gp);
            
            await _context
                        .SaveChangesAsync();
        }

        public IQueryable<Group> Find ()
        {
            return _context
                        .Groups
                        .AsQueryable();
        }

        public async Task Delete (int id)
        {
            var group = await _context
                                    .Groups
                                    .FirstOrDefaultAsync(i => i.Id ==id) 
                                    ?? throw new NullReferenceException();
            _context
                .Remove(group);
        
            await _context
                    .SaveChangesAsync();
        }
        
    }
}
