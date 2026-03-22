using SQLite;

namespace DataPersistence.Database.Models
{
    public class DatabaseNpc
    {
        [PrimaryKey]
        public string Id { get; set; }
    }
}