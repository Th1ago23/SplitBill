using Group = Domain.Entity.Group;

namespace Domain.Interface.Repository
{
    public interface IGroupRepository
    {
        public Task<bool> Create(Group gp);
        public void Update(Group gp);
        public Task Rename(string newName, Group gp);
        public void Delete(Group gp);
        public IQueryable<Group> Find();
    }
}
