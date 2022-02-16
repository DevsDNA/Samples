using Microsoft.AspNetCore.Mvc;
using Todo.Common.Models.Requests;
using Todo.Common.Models.Responses;
using Todo.WebAPI.Database;

namespace Todo.WebAPI.Controllers
{
    [Route("api/tasklist/items/{taskListId:int:min(1)}/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly TodoDatabaseContext dbContext;

        public TaskItemController(TodoDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet]
        [Route("items/{id:int:min(1)}", Name = nameof(GetTaskItemByIdAsync))]
        [ProducesResponseType(typeof(TaskItemDetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskItemDetailResponse>> GetTaskItemByIdAsync(int taskListId, int id)
        {
            return Ok();
        }


        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(IEnumerable<TaskItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TaskItemResponse>>> GetTaskItemsAsync(int taskListId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("items")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateTaskItemAsync(int taskListId, [FromBody] CreateTaskItemRequest request)
        {
            return Ok();
        }

        [HttpPut]
        [Route("items/{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTaskItemAsync(int taskListId, int id, [FromBody] UpdateTaskItemRequest request)
        {
            return NoContent();
        }

        [HttpPut]
        [Route("items/{id:int:min(1)}/change-status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeStatusAsync(int taskListId, int id, [FromBody] Common.Models.Enums.TaskItemStatus status)
        {
            return NoContent();
        }


        [HttpDelete]
        [Route("items/{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTaskListByIdAsync(int taskListId, int id)
        {           
            return NoContent();
        }
    }
}
