namespace Todo.WebAPI.Models
{
    public class TaskItem
    {
        public TaskItem(string title, string description)
        {
            CreationDate = DateTime.Now;
            Status = TaskItemStatus.NotStarted;
            Title = title;
            Description = description;
        }

        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus Status { get; set; }

        public int TaskListId { get; set; }
        public TaskList TaskList { get; set; }
    }
}