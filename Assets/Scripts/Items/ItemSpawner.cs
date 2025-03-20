using System;
using UnityEngine;

namespace Items
{
    public class ItemSpawner : MonoBehaviour
    {
        private static ItemSpawner _instance;

        public static ItemSpawner Instance
        {
            get => _instance;
            private set
            {
                if (_instance is not null)
                {
                    throw new Exception($"Tried to create multiple singletons: {nameof(ItemSpawner)}");
                }
                _instance = value;
            }
        }

        private void Start()
        {
            Instance = this;
        }

        public void SpawnItem(ItemInstanceProperties instance, Vector3 positionToSpawnAt, Quaternion rotation)
        {
            var newItem = Instantiate(instance.Item.Prefab, positionToSpawnAt, rotation);
            var itemBehaviour = newItem.GetComponent<ItemBehaviour>();
            itemBehaviour.InitializeInstance(instance);
        }
    }
}