using Todo.Common.Models.Enums;

namespace Todo.Common.Models.Responses
{
    public class TaskItemDetailResponse
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus Status { get; set; }
    }
}
