using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Model
{
    public partial class ToDo
    { 
        public int Id { get; set; }
        public string Task { get; set; } = null!;
        public sbyte Status { get; set; }
    }
}
