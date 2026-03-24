namespace EmployeeAPI.Models
{
    public class MongoDBSettings
    {
        // ⚠️ These names must EXACTLY match appsettings.json keys
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
