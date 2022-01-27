using Todo.Common.Models.Enums;

namespace Todo.Common.Models.Responses
{
    public class TaskItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TaskItemStatus Status { get; set; }
    }
}