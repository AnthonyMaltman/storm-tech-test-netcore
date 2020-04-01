using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.Entities;
using System.Linq;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoItems;
using Todo.Models.TodoLists;
using Todo.Repositories;
using Todo.Services;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        private readonly ITodoRepository _toDoRepository;
        private readonly IUserStore _userStore;

        public TodoListController(ITodoRepository todoRepository, IUserStore userStore)
        {
            _toDoRepository = todoRepository;
            _userStore = userStore;
        }

        public IActionResult Index()
        {
            var userId = User.Id();
            var todoLists = _toDoRepository.GetRelevantTodoLists(userId);
            var viewmodel = TodoListIndexViewmodelFactory.Create(todoLists);
            return View(viewmodel);
        }

        public IActionResult Detail(int todoListId, bool displayDoneItems = true, bool orderByRank = false)
        {
            var todoList = _toDoRepository.GetTodoList(todoListId);

            var viewmodel = TodoListDetailViewmodelFactory.Create(todoList, displayDoneItems, orderByRank);

            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TodoListFields());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTodoItem(TodoItemCreateFields fields)
        {
            var item = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance, 0);

            try
            {
                await _toDoRepository.AddAsync(item);

                return Json(new { success = "success" });
            }
            catch(Exception ex)
            {
                return Json(new { error = "could not create todo item." });
            }
        }

        public class Items
        {
            public int TodoItemId { get; set; }
            public int Rank { get; set; }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRankOrder(int TodoListId, IEnumerable<Items> Items)
        {
            try
            {
                var todoList = _toDoRepository.GetTodoList(TodoListId);

                foreach(var todoItem in todoList.Items)
                {
                    todoItem.Rank = Items.Single(x => x.TodoItemId == todoItem.TodoItemId).Rank;
                    await _toDoRepository.UpdateAsync(todoItem);
                }

                todoList = _toDoRepository.GetTodoList(TodoListId);

                return Json(new { success = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { error = "could not save todo item." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoListFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            var currentUser = await _userStore.GetUserAsync(User.Id());

            var todoList = new TodoList(currentUser, fields.Title);

            await _toDoRepository.AddAsync(todoList);

            return RedirectToAction("Create", "TodoItem", new {todoList.TodoListId});
        }
    }
}