﻿using Motivator.DB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motivator.DB.Repositories.Impl
{
    public class TodoRepository : ITodoRepository
    {
        public MotivatorContext Context { get; set; }

        public TodoRepository(MotivatorContext context)
        {
            this.Context = context;
        }

        public IEnumerable<Todo> GetAll(int ownerId)
        {
            return Context.Todos.Where(x => x.OwnerId == ownerId).ToList();
        }

        public async Task Add(Todo model)
        {
            await Context.Todos.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task<Todo> Get(int taskId)
        {
            return await Context.Todos.FindAsync(taskId);
        }

        public async Task AddChild(int parentId, int childId)
        {
            var parent = await Get(parentId);
            parent.SubTodoIds.Add(childId);
            await Context.SaveChangesAsync();
        }
    }
}