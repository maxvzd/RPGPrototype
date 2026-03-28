using Registries;
using UnityEngine;

namespace DataPersistence.SmartObjects
{
    public class SmartObject
    {
        public string Key { get; }

        public GameObject GameObject { get; }
        
        public SmartObject(string key, GameObject gameObject)
        {
            Key = key;
            GameObject = gameObject;
            if (!SmartObjectRegistry.Register(this))
            {
                Debug.Log($"Failed to register {key}");
            }
        }
    }
}