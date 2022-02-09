namespace Todo.Common.Models.Responses
{
    public class TaskListDetailResponse
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public List<TaskItemResponse> Tasks { get; set; }
    }
}
