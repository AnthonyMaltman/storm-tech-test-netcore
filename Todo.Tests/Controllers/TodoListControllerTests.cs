using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Todo.Controllers;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoItems;
using Todo.Models.TodoLists;
using Todo.Repositories;
using Xunit;

namespace Todo.Tests.Controllers
{
    public class TodoListControllerTests
    {
        [Fact]
        public void Detail_ReturnsAllToDoItems_WhenCalledWIthDisplayDoneItemsSetTrue()
        {
            var user = new IdentityUser("alice@example.com");
            var todoList = new TodoList(user, "shopping");
            todoList.Items.Add(new TodoItem(1, user, "Get eggs", Importance.High, 1, false));
            todoList.Items.Add(new TodoItem(1, user, "Get bread", Importance.High, 2, true));
            todoList.Items.Add(new TodoItem(1, user, "Get milk", Importance.High, 3, false));

            var todo = TodoListDetailViewmodelFactory.Create(todoList, true, false);

            var userStore = new Mock<IUserStore>();

            var todoRepository = new Mock<ITodoRepository>();
            todoRepository.Setup(x => x.GetTodoList(It.IsAny<int>())).Returns(todoList);

            var sut = new TodoListController(todoRepository.Object, userStore.Object);

            var result = sut.Detail(1, true) as ViewResult;

            var Model = result.Model as TodoListDetailViewmodel;

            Assert.True(Model.Items.Count == 3);
        }

        [Fact]
        public void Detail_ReturnsToDoItemsThatAreNotDone_WhenCalledWIthDisplayDoneItemsSetFalse()
        {
            var user = new IdentityUser("alice@example.com");
            var todoList = new TodoList(user, "shopping");
            todoList.Items.Add(new TodoItem(1, user, "Get eggs", Importance.High, 1, false));
            todoList.Items.Add(new TodoItem(1, user, "Get bread", Importance.High, 2, true));
            todoList.Items.Add(new TodoItem(1, user, "Get milk", Importance.High, 3, false));

            var todo = TodoListDetailViewmodelFactory.Create(todoList, true, false);

            var userStore = new Mock<IUserStore>();

            var todoRepository = new Mock<ITodoRepository>();
            todoRepository.Setup(x => x.GetTodoList(It.IsAny<int>())).Returns(todoList);

            var sut = new TodoListController(todoRepository.Object, userStore.Object);

            var result = sut.Detail(1, false) as ViewResult;

            var Model = result.Model as TodoListDetailViewmodel;

            Assert.True(Model.Items.Count == 2);
        }
    }
}
