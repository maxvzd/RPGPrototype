using System.Collections.Generic;
using DataPersistence.Database.Models;
using SQLite;

namespace DataPersistence.Database
{
    public class DatabaseRepository
    {
        private readonly SQLiteConnection _connection = new(@"C:\Users\Max\Documents\Unity\RPGPrototype\Assets\Database\RPGPrototype.db");
        
            
        public IEnumerable<DatabaseNpc> GetNpcs()
        {
            return _connection.Query<DatabaseNpc>("SELECT n.Id, n.Energy, n.Money, n.Hunger, h.Key as HomeKey, w.Key as WorkKey, f.Key as FoodKey, Name, sa.Key as SpawnKey FROM NPCs as n INNER JOIN SmartGameObjectKeys as h ON n.HomeKey = h.Id INNER JOIN SmartGameObjectKeys as w ON n.WorkKey = w.Id INNER JOIN SmartGameObjectKeys as f ON n.FoodKey = f.Id INNER JOIN SpawnAnchors as sa ON n.Spawn = sa.Id");
        }
    }
}