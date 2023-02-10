using System;
namespace ToDoList.Model
{
    public class TasksModel
    {
        public int Id { get; set; }
        public string Task { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Approval { get; set; } = null!;
        public int UserId { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is TasksModel model &&
                   Id == model.Id &&
                   Task == model.Task &&
                   Status == model.Status &&
                   Approval == model.Approval &&
                   UserId == model.UserId;
        }
    }


}

