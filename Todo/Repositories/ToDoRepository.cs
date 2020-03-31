using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Services;

namespace Todo.Repositories
{
    public interface ITodoRepository
    {
        Task AddAsync(TodoItem todoItem);
        Task AddAsync(TodoList todoList);
        Task UpdateAsync(TodoItem todoItem);
        TodoList GetTodoList(int todoListId);
        TodoItem GetTodoItem(int todoItemId);

        IQueryable<TodoList> GetRelevantTodoLists(string userId);
    }
    public class TodoRepository : ITodoRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public TodoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TodoList> GetRelevantTodoLists(string userId)
        {
            return _dbContext.RelevantTodoLists(userId);
        }

        public TodoList GetTodoList(int todoListId)
        {
            return _dbContext.SingleTodoList(todoListId);
        }

        public TodoItem GetTodoItem(int todoItemId)
        {
            return _dbContext.SingleTodoItem(todoItemId);
        }

        public async Task AddAsync(TodoList todoIList)
        {
            await _dbContext.AddAsync(todoIList);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(TodoItem todoItem)
        {
            await _dbContext.AddAsync(todoItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem todoItem)
        {
            _dbContext.Update(todoItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
