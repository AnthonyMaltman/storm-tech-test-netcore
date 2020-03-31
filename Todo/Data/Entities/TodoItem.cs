﻿using Microsoft.AspNetCore.Identity;

namespace Todo.Data.Entities {
    public class TodoItem
    {
        public int TodoItemId { get; set; }
        public string Title { get; set; }
        public string ResponsiblePartyId { get; set; }
        public IdentityUser ResponsibleParty { get; set; }
        public bool IsDone { get; set; }
        public Importance Importance { get; set; }

        public int TodoListId { get; set; }
        public TodoList TodoList { get; set; }

        public int Rank { get; set; }

        protected TodoItem() { }

        public TodoItem(int todoListId, string responsiblePartyId, string title, Importance importance, int rank, bool isDone = false)
        {
            TodoListId = todoListId;
            ResponsiblePartyId = responsiblePartyId;
            Title = title;
            Importance = importance;
            IsDone = isDone;
            Rank = rank;
        }

        public TodoItem(int todoListId, IdentityUser responsibleParty, string title, Importance importance, int rank, bool isDone = false)
        {
            TodoListId = todoListId;
            ResponsibleParty = responsibleParty;
            ResponsiblePartyId = responsibleParty.Id;
            Title = title;
            Importance = importance;
            IsDone = isDone;
            Rank = rank;
        }
    }
}