using Motivator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motivator.DB.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> GetAll(int ownerId);

        void AddTask(TaskModel model);
    }
}
