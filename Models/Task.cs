using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TaskManagementApi.Models
{
    public class Task
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        
        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public DateTime DueDate { get; set; }
        
        public bool IsCompleted { get; set; }
    }
}
