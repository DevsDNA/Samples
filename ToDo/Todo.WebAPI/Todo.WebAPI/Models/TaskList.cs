namespace Todo.WebAPI.Models
{
    public class TaskList
    {
        public TaskList(string title)
        {
            CreationDate = DateTime.Now;
            Title = title;
        }

        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
}
