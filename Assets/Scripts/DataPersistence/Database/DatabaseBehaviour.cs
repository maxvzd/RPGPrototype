using System;
using UnityEngine;

namespace DataPersistence.Database
{
    public class DatabaseBehaviour : MonoBehaviour
    {
        private DatabaseRepository _database;

        private void Awake()
        {
            _database = new DatabaseRepository();
            var npcs = _database.GetNpcs();
        }
    }
}