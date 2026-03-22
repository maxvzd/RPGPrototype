using SQLite;

namespace DataPersistence.Database.Models
{
    public class DatabaseNpc
    {
        [PrimaryKey]
        public string Id { get; set; }
        public float Energy { get; set; }
        public float Money { get; set; }
        public float Hunger { get; set; }
        public string HomeKey { get; set; }
        public string WorkKey { get; set; }
        public string FoodKey { get; set; }
        public string Name { get; set; }
        public string SpawnKey { get; set; }
    }
}