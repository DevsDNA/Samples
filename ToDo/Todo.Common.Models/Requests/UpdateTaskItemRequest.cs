using Todo.Common.Models.Enums;

namespace Todo.Common.Models.Requests
{
    public class UpdateTaskItemRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus Status { get; set; }
    }
}
