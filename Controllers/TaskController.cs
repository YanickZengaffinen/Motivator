using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motivator.DB.Repositories;
using Motivator.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Motivator.Controllers
{
    [Route("task")]
    [AutoValidateAntiforgeryToken]
    public class TaskController : Controller
    {
        private readonly ITaskRepository taskRepo;

        public TaskController(ITaskRepository taskRepo)
        {
            this.taskRepo = taskRepo;
        }

        [Authorize]
        [Route("new")]
        public IActionResult New()
        {
            var taskModel = new TaskModel()
            {
                OwnerId = GetUserId()
            };

            return View(taskModel);
        }

        [Authorize]
        [HttpPost]
        [Route("new")]
        public IActionResult New(TaskModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalid", "Invalid input");
                return View(model);
            }

            if(model.OwnerId == GetUserId())
            {
                taskRepo.AddTask(model);
            }

            return RedirectToAction("Overview", "Task");
        }

        [Authorize]
        [Route("overview")]
        public IActionResult Overview()
        {
            int userId = GetUserId();
            if (userId >= 0)
            {
                var tasks = taskRepo.GetAll(userId)
                    .OrderBy(x => x.Title)
                    .ToList();
                ViewData["tasks"] = tasks;
            }

            return View();
        }

        private int GetUserId()
        {
            return int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}