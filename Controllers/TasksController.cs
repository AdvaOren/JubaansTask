using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories;
using TaskModel = TaskManagementApi.Models.Task;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            var tasks = await _taskRepository.GetTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async System.Threading.Tasks.Task<ActionResult<TaskModel>> GetTask(string id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult<TaskModel>> CreateTask(TaskModel task)
        {
            if (string.IsNullOrEmpty(task.Title))
            {
                return BadRequest("Title should not be empty.");
            }

            if (task.DueDate < System.DateTime.UtcNow)
            {
                return BadRequest("Due Date should not be in the past.");
            }

            await _taskRepository.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> UpdateTask(string id, TaskModel task)
        {
            var existingTask = await _taskRepository.GetTaskByIdAsync(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(task.Title))
            {
                return BadRequest("Title should not be empty.");
            }

            if (task.DueDate < System.DateTime.UtcNow)
            {
                return BadRequest("Due Date should not be in the past.");
            }

            task.Id = existingTask.Id;
            await _taskRepository.UpdateTaskAsync(id, task);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> DeleteTask(string id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            await _taskRepository.DeleteTaskAsync(id);

            return NoContent();
        }
    }
}

