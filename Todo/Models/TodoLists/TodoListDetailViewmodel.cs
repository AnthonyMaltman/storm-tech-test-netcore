using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Todo.Data.Entities;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string TodoListTitle { get; }
        public bool DisplayDoneItems { get; }
        public bool OrderByRank { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }

        [Display(Name = "Responsible Party")]
        public string ResponsiblePartyId { get; set; }
        public Importance Importance { get; set; } = Importance.Medium;
        public string Title { get; }

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, bool displayDoneItems, bool orderByRank)
        {
            Items = items;
            TodoListId = todoListId;
            TodoListTitle = title;
            DisplayDoneItems = displayDoneItems;
            OrderByRank = orderByRank;
        }
    }
}