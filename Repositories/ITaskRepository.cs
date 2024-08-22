using System.Collections.Generic;
using System.Threading.Tasks;
using TaskModel = TaskManagementApi.Models.Task;

namespace TaskManagementApi.Repositories
{
    /// <summary>
    /// ITaskRepository Interface
    /// 
    /// This interface defines the contract for a task repository. It includes methods
    /// for retrieving, creating, updating, and deleting tasks asynchronously. 
    /// The repository operates on TaskModel objects, which represent tasks in the task management system.
    /// </summary>
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetTasksAsync();
        Task<TaskModel> GetTaskByIdAsync(string id);
        Task CreateTaskAsync(TaskModel task);
        Task UpdateTaskAsync(string id, TaskModel task);
        Task DeleteTaskAsync(string id);
    }
}
