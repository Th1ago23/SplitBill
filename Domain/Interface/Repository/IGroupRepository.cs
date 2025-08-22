using Domain.Entity;
using System.Text.RegularExpressions;
using Group = Domain.Entity.Group;

namespace Domain.Interface.Repository
{
    public interface IGroupRepository
    {
        public Task<bool> Create(Group gp);
        public Task Update(Group gp);
        public Task Rename(string newName, Group gp);
        public Task Delete(int id);
        public IQueryable<Group> Find();
    }
}
