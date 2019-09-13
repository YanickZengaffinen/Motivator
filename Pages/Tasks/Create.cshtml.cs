using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Motivator.DB.Models;
using Motivator.DB.Repositories;
using Motivator.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace Motivator.Pages.Tasks
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class CreateModel : PageModel
    {
        public int OwnerId { get; set; }

        public int? SubTaskId { get; set; }

        [BindProperty]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; } = DateTime.Now;

        [BindProperty]
        [Required(ErrorMessage = "Task must have a title")]
        public string Title { get; set; }

        public string Description { get; set; }

        [BindProperty]
        public bool UseDueDate { get; set; }

        private readonly IAuthService authService;
        private readonly ITaskRepository taskRepo;
        private readonly IMapper mapper;

        public CreateModel(IAuthService authService, ITaskRepository taskRepo, IMapper mapper)
        {
            this.authService = authService;
            this.taskRepo = taskRepo;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if(authService.TryGetUserId(User, out int userId))
            {
                var task = mapper.Map<Task>(this);
                task.OwnerId = userId;

                if (!UseDueDate)
                {
                    task.DueDate = null;
                }

                taskRepo.AddTask(task);

                return RedirectToPage("Overview");
            }

            return Page();
        }
    }
}