using Motivator.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motivator.DB.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAll(int ownerId);

        Task<IEnumerable<Todo>> GetHierarchy(int ownerId, int? parentId);

        Task Add(Todo model);

        Task<Todo> Get(int taskId);

        Task Update(Todo model);
    }
}
