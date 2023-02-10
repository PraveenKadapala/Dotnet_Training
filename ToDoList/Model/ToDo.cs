using System;
using System.Collections.Generic;

namespace ToDoList.Model
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Task { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Approval { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
