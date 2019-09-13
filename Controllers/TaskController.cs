using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motivator.DB.Repositories;
using Motivator.Util.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Task = Motivator.DB.Models.Task;

namespace Motivator.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [IgnoreAllExcept(typeof(Task), nameof(Task.Title))]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository taskRepo;

        public TaskController(ITaskRepository taskRepo)
        {
            this.taskRepo = taskRepo;
        }

        [HttpGet]
        public IEnumerable<Task> Get()
        {
            if(GetUserId(out int userId))
            {
                return taskRepo.GetAll(userId);
            }

            return new List<Task>();
        }

        private bool GetUserId(out int userId)
        {
            if (User?.Identity.IsAuthenticated == true)
            {
                userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                return true;
            }

            userId = -1;
            return false;
        }
    }
}
