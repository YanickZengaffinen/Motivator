using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motivator.DB.Repositories;
using Motivator.Services;
using Motivator.Util.Json;
using System.Collections.Generic;
using Task = Motivator.DB.Models.Task;

namespace Motivator.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ITaskRepository taskRepo;

        public TaskController(IAuthService authService, ITaskRepository taskRepo)
        {
            this.authService = authService;
            this.taskRepo = taskRepo;
        }

        [HttpGet]
        [IgnoreAllExceptFilterFactory(
            Ignored = typeof(Task), 
            Except = new string[] { nameof(Task.Title), nameof(Task.DueDate) })]
        public IEnumerable<Task> Get()
        {
            if(authService.TryGetUserId(User, out int userId))
            {
                return taskRepo.GetAll(userId);
            }

            return new List<Task>();
        }
    }
}
