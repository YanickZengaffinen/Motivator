using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motivator.DB.Models;
using Motivator.DB.Repositories;
using Motivator.Services;
using Motivator.Util.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motivator.Controllers
{
    [Route("api/todos")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ITodoRepository todoRepo;

        public TodoController(IAuthService authService, ITodoRepository todoRepo)
        {
            this.authService = authService;
            this.todoRepo = todoRepo;
        }

        [HttpGet]
        [IgnoreAllExceptFilterFactory(
            Ignored = typeof(Todo), 
            Except = new string[] { nameof(Todo.Id), nameof(Todo.Title), nameof(Todo.DueDate), nameof(Todo.IsCompleted) })]
        public async Task<IEnumerable<Todo>> GetAsync()
        {
            if(authService.TryGetUserId(User, out int userId))
            {
                return await todoRepo.GetAll(userId);
            }

            return new List<Todo>();
        }

        [HttpGet("complete")]
        public async Task Complete(int taskId, bool isComplete)
        {
            if(authService.TryGetUserId(User, out int userId))
            {
                var task = await todoRepo.Get(taskId);
                if (task.OwnerId == userId)
                {
                    task.IsCompleted = isComplete;
                    task.CompletionDate = isComplete ? (DateTime?)DateTime.Now : null;
                    await todoRepo.Update(task);
                }
            }
        }
    }
}
