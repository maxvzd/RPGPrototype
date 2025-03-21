using System.Collections.Generic;
using Items.InstancePropertiesClasses;
using UnityEngine;

namespace Items
{
    public class Inventory : MonoBehaviour
    {
        private const float WEIGHT_LIMIT = 500;
        private float _currentWeight = 0;
        private readonly List<InstanceProperties> _items = new();

        public IReadOnlyList<InstanceProperties> Items => _items;
    
        public bool AddItem(InstanceProperties instance)
        {
            if (!(_currentWeight + instance.BaseItemProperties.Weight <= WEIGHT_LIMIT))
            {
                Debug.Log($"Couldn't add {instance.BaseItemProperties.ItemName} as it's too heavy, Current weight: {_currentWeight}");
                return false;
            }

            // Instance persistence test
            // if (instance is ItemInstanceProperties itemInstance)
            // {
            //     Debug.Log($"Before: {itemInstance.MyMessage}");
            //     itemInstance.SetMessage();
            //     Debug.Log($"After: {itemInstance.MyMessage}");
            // }
            // if (instance is WeaponInstanceProperties itemInstance)
            // {
            //     Debug.Log($"Before: {itemInstance.Durability}");
            //     itemInstance.Degrade();
            //     Debug.Log($"After: {itemInstance.Durability}");
            // }
            
            _currentWeight += instance.BaseItemProperties.Weight;
            _items.Add(instance);
            return true;
        }
    
        public bool RemoveItem(InstanceProperties instance)
        {
            if (!_items.Exists(x => x == instance)) return false;
        
            _currentWeight -= instance.BaseItemProperties.Weight;
            _items.Remove(instance);
            
            // Instance persistence test
            // if (instance is ItemInstanceProperties itemInstance)
            // {
            //     Debug.Log($"Retrieved from invent: {itemInstance.MyMessage}");
            // }
            // if (instance is WeaponInstanceProperties itemInstance)
            // {
            //     Debug.Log($"Retrieved from invent: {itemInstance.Durability}");
            // }
            
            return true;
        }
    }
}