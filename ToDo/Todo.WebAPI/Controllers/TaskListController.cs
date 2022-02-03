using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Common.Models.Requests;
using Todo.Common.Models.Responses;
using Todo.WebAPI.Database;

namespace Todo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        private readonly TodoDatabaseContext dbContext;

        public TaskListController(TodoDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("items/{id:int:min(1)}")]
        [ProducesResponseType(typeof(TaskListDetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskListDetailResponse>> GetTaskListByIdAsync(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(TaskListResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<TaskListResponse>> GetTaskListsAsync()
        {
            return Ok();
        }


        [HttpPost]
        [Route("items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateTaskListAsync([FromBody] CreateTaskListRequest request)
        {
            return Ok();
        }

        [HttpPut]
        [Route("items/{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateTaskListAsync(int id, [FromBody] UpdateTaskListRequest request)
        {
            return NoContent();
        }

        [HttpDelete]
        [Route("items/{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTaskListAsync(int id)
        {
            return NoContent();
        }
    }
}
