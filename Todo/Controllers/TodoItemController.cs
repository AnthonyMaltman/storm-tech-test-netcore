using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoItems;
using Todo.Repositories;
using Todo.Services;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoItemController : Controller
    {
        private readonly ITodoRepository _toDoRepository;

        public TodoItemController(ITodoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        [HttpGet]
        public IActionResult Create(int todoListId)
        {
            var todoList = _toDoRepository.GetTodoList(todoListId);
            var fields = TodoItemCreateFieldsFactory.Create(todoList, User.Id());
            return View(fields);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoItemCreateFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            var item = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance, 0);

            await _toDoRepository.AddAsync(item);

            return RedirectToListDetail(fields.TodoListId);
        }

        [HttpGet]
        public IActionResult Edit(int todoItemId)
        {
            var todoItem = _toDoRepository.GetTodoItem(todoItemId);
            var fields = TodoItemEditFieldsFactory.Create(todoItem);
            return View(fields);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoItemEditFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            var todoItem = _toDoRepository.GetTodoItem(fields.TodoItemId);

            TodoItemEditFieldsFactory.Update(fields, todoItem);

            await _toDoRepository.UpdateAsync(todoItem);

            return RedirectToListDetail(todoItem.TodoListId);
        }

        private RedirectToActionResult RedirectToListDetail(int fieldsTodoListId)
        {
            return RedirectToAction("Detail", "TodoList", new {todoListId = fieldsTodoListId});
        }
    }
}