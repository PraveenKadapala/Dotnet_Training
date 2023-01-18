using System;
namespace ToDoList.Model
{
	public class tasksModel
	{
        public int Id { get; set; }
        public string Task { get; set; } = null!;
        public sbyte Status { get; set; }
        public sbyte Approval { get; set; }
        public int UserId { get; set; }
    }
}

