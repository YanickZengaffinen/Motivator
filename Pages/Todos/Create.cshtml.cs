using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Motivator.DB.Models;
using Motivator.DB.Repositories;
using Motivator.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Motivator.Pages.Todos
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class CreateModel : PageModel
    {
        public int OwnerId { get; set; }

        public IList<int> SubTodoIds { get; set; }

        [BindProperty]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; } = DateTime.Now;

        [BindProperty]
        [Required(ErrorMessage = "Todo must have a title")]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public bool UseDueDate { get; set; }

        [BindProperty]
        public int? ParentTodoId { get; set; }

        private readonly IAuthService authService;
        private readonly ITodoRepository todoRepo;
        private readonly IMapper mapper;

        public CreateModel(IAuthService authService, ITodoRepository todoRepo, IMapper mapper)
        {
            this.authService = authService;
            this.todoRepo = todoRepo;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if(authService.TryGetUserId(User, out int userId))
            {
                var todo = mapper.Map<Todo>(this);
                todo.OwnerId = userId;

                if (!UseDueDate)
                {
                    todo.DueDate = null;
                }

                await todoRepo.Add(todo);

                return RedirectToPage("Overview");
            }

            return Page();
        }
    }
}