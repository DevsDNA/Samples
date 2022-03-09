using Todo.Common.Models.Responses;
using Todo.WebAPI.Models;

namespace Todo.WebAPI.ModelExtension
{
    public static class TaskListExtensions
    {
        public static TaskListDetailResponse ToTaskListDetailResponse(this TaskList taskList)
        {
            return new TaskListDetailResponse()
            {
                Id = taskList.Id,
                CreationDate = taskList.CreationDate,
                Title = taskList.Title,
                Tasks = taskList.Tasks.Select(t => new TaskItemResponse()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status.ToTaskItemStatus()
                }).ToList()
            };
        }

        public static TaskListResponse ToTaskListResponse(this TaskList taskList)
        {
            return new TaskListResponse()
            {
                Id = taskList.Id,
                Title = taskList.Title
            };
        }
    }
}
