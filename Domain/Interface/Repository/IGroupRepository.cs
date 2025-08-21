using System.Text.RegularExpressions;

namespace Domain.Interface.Repository
{
    public interface IGroupRepository
    {
        public Task<bool> Create(Group gp);
        Task Update(Group gp);
        Task Rename(string newName, Group gp);
        Task Delete(int id);
        Task<Group> Find(int id);
        Task<IEnumerable<Group>> FindAll();
    }
}
