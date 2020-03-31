using System.Collections.Generic;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public bool DisplayDoneItems { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, bool displayDoneItems)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
            DisplayDoneItems = displayDoneItems;
        }
    }
}