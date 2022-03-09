using System.ComponentModel;

namespace Todo.WebAPI.ModelExtension
{
    public static class TaskItemStatusExtensions
    {
        public static Common.Models.Enums.TaskItemStatus ToTaskItemStatus(this Models.TaskItemStatus taskItemStatus)
        {
            switch (taskItemStatus)
            {
                case Models.TaskItemStatus.NotStarted:
                    return Common.Models.Enums.TaskItemStatus.NotStarted;
                case Models.TaskItemStatus.InProgress:
                    return Common.Models.Enums.TaskItemStatus.InProgress;
                case Models.TaskItemStatus.Completed:
                    return Common.Models.Enums.TaskItemStatus.Completed;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
