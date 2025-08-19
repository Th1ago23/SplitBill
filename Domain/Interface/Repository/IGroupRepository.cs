using System.Text.RegularExpressions;

namespace Domain.Interface.Repository
{
    public interface IGroupRepository
    {
        Task Add(Group gp);
        Task Update(Group gp);
        Task Delete(int id);
        Task<Group> Find(int id);
        Task<IEnumerable<Group>> FindAll();
    }
}
