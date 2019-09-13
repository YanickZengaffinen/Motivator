using Motivator.DB.Models;
using Motivator.Models;
using System.Collections.Generic;
using System.Linq;

namespace Motivator.DB.Repositories.Impl
{
    public class TaskRepository : ITaskRepository
    {
        public MotivatorContext Context { get; set; }

        public TaskRepository(MotivatorContext context)
        {
            this.Context = context;
        }

        public IEnumerable<Task> GetAll(int ownerId)
        {
            return Context.Tasks.Where(x => x.OwnerId == ownerId).ToList();
        }

        public void AddTask(Task model)
        {
            Context.Tasks.Add(model);
            Context.SaveChanges();
        }
    }
}
