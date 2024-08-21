using MongoDB.Driver;
using TaskManagementApi.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TaskModel = TaskManagementApi.Models.Task;

namespace TaskManagementApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<TaskModel> _tasksCollection;

        public TaskRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _tasksCollection = mongoDatabase.GetCollection<TaskModel>(databaseSettings.Value.TasksCollectionName);
        }

        public async System.Threading.Tasks.Task<IEnumerable<TaskModel>> GetTasksAsync()
        {
            return await _tasksCollection.Find(_ => true).ToListAsync();
        }

        public async System.Threading.Tasks.Task<TaskModel> GetTaskByIdAsync(string id)
        {
            return await _tasksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task CreateTaskAsync(TaskModel task)
        {
            await _tasksCollection.InsertOneAsync(task);
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(string id, TaskModel task)
        {
            await _tasksCollection.ReplaceOneAsync(x => x.Id == id, task);
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(string id)
        {
            await _tasksCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
