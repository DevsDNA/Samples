using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Common.Models.Requests;
using Todo.Common.Models.Responses;
using Todo.WebAPI.Database;
using Todo.WebAPI.ModelExtension;
using Todo.WebAPI.Models;

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
        [Route("items/{id:int:min(1)}", Name = nameof(GetTaskListByIdAsync))]
        [ProducesResponseType(typeof(TaskListDetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskListDetailResponse>> GetTaskListByIdAsync(int id)
        {
            var taskList = await dbContext.TaskLists.Include(tl => tl.Tasks)
                                                    .FirstOrDefaultAsync(tl => tl.Id == id);

            if (taskList == null)
            {
                return NotFound();
            }

            return Ok(taskList.ToTaskListDetailResponse());
        }

        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(TaskListResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TaskListResponse>>> GetTaskListsAsync()
        {
            var tasksList = await dbContext.TaskLists.ToListAsync();

            return Ok(tasksList.Select(tl => tl.ToTaskListResponse()));
        }


        [HttpPost]
        [Route("items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateTaskListAsync([FromBody] CreateTaskListRequest request)
        {
            if(string.IsNullOrEmpty(request.Title))
            {
                return BadRequest("Title must not be empty.");
            }

            var taskList = new TaskList(request.Title);

            await dbContext.TaskLists.AddAsync(taskList);
            var createdId = await dbContext.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetTaskListByIdAsync), new { id = createdId}, null);
        }

        [HttpPut]
        [Route("items/{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateTaskListAsync(int id, [FromBody] UpdateTaskListRequest request)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                return BadRequest("Title must not be empty.");
            }

            var taskList = await dbContext.TaskLists.FindAsync(id);
            if(taskList == null)
            {
                return NotFound();
            }

            taskList.Title = request.Title;

            dbContext.TaskLists.Update(taskList);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("items/{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTaskListAsync(int id)
        {
            var taskList = await dbContext.TaskLists.FindAsync(id);
            if (taskList == null)
            {
                return NotFound();
            }

            dbContext.TaskLists.Remove(taskList);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
