
using Registries;
using UnityEngine;

namespace DataPersistence.Spawning
{
    public class SpawnAnchor
    {
        public string Key { get; }

        public Transform Transform { get; }
        
        public SpawnAnchor(string key, Transform transform)
        {
            Key = key;
            Transform = transform;
            
            if (!SpawnAnchorRegistry.Register(this))
            {
                Debug.Log($"Failed to register {key}");
            }
        }
    }
}