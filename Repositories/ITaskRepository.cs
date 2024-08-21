using System.Collections.Generic;
using System.Threading.Tasks;
using TaskModel = TaskManagementApi.Models.Task;

namespace TaskManagementApi.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetTasksAsync();
        Task<TaskModel> GetTaskByIdAsync(string id);
        Task CreateTaskAsync(TaskModel task);
        Task UpdateTaskAsync(string id, TaskModel task);
        Task DeleteTaskAsync(string id);
    }
}
