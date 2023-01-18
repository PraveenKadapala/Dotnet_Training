using System;
using System.Collections.Generic;

namespace ToDoList.Model
{
    public partial class User
    {
        public User()
        {
            ToDos = new HashSet<ToDo>();
        }

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Id { get; set; }
        public sbyte Admin { get; set; }

        public virtual ICollection<ToDo> ToDos { get; set; }
    }
}
