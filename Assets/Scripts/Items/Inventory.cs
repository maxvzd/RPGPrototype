using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Inventory : MonoBehaviour
    {
        private const float WEIGHT_LIMIT = 10;
        private float _currentWeight = 0;
        private readonly List<ItemInstanceProperties> _items = new();

        public IEnumerable<ItemInstanceProperties> Items => _items;
    
        public bool AddItem(ItemInstanceProperties instance)
        {
            if (!(_currentWeight + instance.Item.Weight <= WEIGHT_LIMIT))
            {
                Debug.Log($"Couldn't add {instance.Item.ItemName} as it's too heavy, Current weight: {_currentWeight}");
                return false;
            }

            _currentWeight += instance.Item.Weight;
            _items.Add(instance);
            return true;
        }
    
        public bool RemoveItem(ItemInstanceProperties instance)
        {
            if (!_items.Exists(x => x == instance)) return false;
        
            _currentWeight -= instance.Item.Weight;
            _items.Remove(instance);
            
            var currTransform = transform;
            var positionToSpawnAt = currTransform.position + currTransform.forward * 0.5f + currTransform.up * 1.5f;
            ItemSpawner.Instance.SpawnItem(instance, positionToSpawnAt, Quaternion.identity);
            
            return true;
        }
    }
}