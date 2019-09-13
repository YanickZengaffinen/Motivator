using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motivator.Models
{
    public interface ITask
    {
        int OwnerId { get; set; }

        int? SubTaskId { get; set; }

        DateTime? DueDate { get; set; }

        string Title { get; set; }

        string Description { get; set; }
    }
}
