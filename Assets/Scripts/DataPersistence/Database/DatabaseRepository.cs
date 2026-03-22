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
            return _connection.Query<DatabaseNpc>("SELECT * FROM NPCs");
        }
    }
}