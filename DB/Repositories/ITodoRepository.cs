using Motivator.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motivator.DB.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAll(int ownerId);

        Task Add(Todo model);

        Task<Todo> Get(int taskId);

        Task Update(Todo model);

        Task AddChild(int parentId, int childId);
    }
}
