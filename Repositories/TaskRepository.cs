using MongoDB.Driver;
using TaskManagementApi.Models;
using Microsoft.Extensions.Options;
using TaskModel = TaskManagementApi.Models.Task;

namespace TaskManagementApi.Repositories
{
    /// <summary>
    /// TaskRepository Class
    /// 
    /// This class implements the ITaskRepository interface and provides methods 
    /// to interact with the MongoDB database for managing tasks. It includes 
    /// methods to get, create, update, and delete tasks from the database.
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<TaskModel> _tasksCollection;

        /// <summary>
        /// Constructor for TaskRepository.
        /// Initializes a new instance of the TaskRepository class and sets up the MongoDB collection.
        /// </summary>
        /// <param name="databaseSettings">The settings for connecting to the MongoDB database.</param>
        public TaskRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _tasksCollection = mongoDatabase.GetCollection<TaskModel>(databaseSettings.Value.TasksCollectionName);
        }

        /// <summary>
        /// Retrieves all tasks from the database asynchronously.
        /// </summary>
        /// <returns>A task representing an asynchronous operation that contains an enumerable list of TaskModel objects.</returns>
        public async System.Threading.Tasks.Task<IEnumerable<TaskModel>> GetTasksAsync()
        {
            return await _tasksCollection.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Retrieves a task by its ID from the database asynchronously.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
        /// <returns>A task representing an asynchronous operation that contains the TaskModel object with the specified ID.</returns>
        public async System.Threading.Tasks.Task<TaskModel> GetTaskByIdAsync(string id)
        {
            return await _tasksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Creates a new task in the database asynchronously.
        /// </summary>
        /// <param name="task">The TaskModel object representing the task to be created.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public async System.Threading.Tasks.Task CreateTaskAsync(TaskModel task)
        {
            Console.WriteLine($"Status: {task}");

            await _tasksCollection.InsertOneAsync(task);
        }

        /// <summary>
        /// Updates an existing task in the database asynchronously.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="task">The TaskModel object containing the updated task details.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public async System.Threading.Tasks.Task UpdateTaskAsync(string id, TaskModel task)
        {
            await _tasksCollection.ReplaceOneAsync(x => x.Id == id, task);
        }

        /// <summary>
        /// Deletes a task by its ID from the database asynchronously.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        public async System.Threading.Tasks.Task DeleteTaskAsync(string id)
        {
            await _tasksCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
