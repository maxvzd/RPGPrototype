
using UnityEngine;

namespace DataPersistence.Spawning
{
    public class SpawnAnchor
    {
        public string Key { get; }

        private Vector3 _position;
        
        public SpawnAnchor(string key, Vector3 position)
        {
            Key = key;
            _position = position;
            
            if (!SpawnAnchorRegistry.Register(this))
            {
                Debug.Log($"Failed to register {key}");
            }
        }
    }
}