using Motivator.DB.Models;
using System.Collections.Generic;

namespace Motivator.DB.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<Task> GetAll(int ownerId);

        void AddTask(Task model);
    }
}
