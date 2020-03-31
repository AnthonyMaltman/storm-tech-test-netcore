using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, bool displayDoneItems, bool orderByRank)
        {
            var items = todoList.Items.Where(x => displayDoneItems ? true : x.IsDone == false).Select(TodoItemSummaryViewmodelFactory.Create).ToList();

            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items, displayDoneItems, orderByRank);
        }
    }
}