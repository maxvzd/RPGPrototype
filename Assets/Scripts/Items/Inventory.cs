using System;
using System.Collections.Generic;
using System.Linq;
using Items.ItemInstances;
using Items.ItemScriptableObjects;
using Registries;
using Unity.VisualScripting;
using UnityEngine;

namespace Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<ItemInstanceScriptableObject> startingItems;
        [SerializeField] private float weightLimit = 500;
        private float _currentWeight = 0;
        private readonly Dictionary<Guid, BaseItemInstance> _items = new();

        public IReadOnlyDictionary<Guid, BaseItemInstance> Items => _items;

        private void Start()
        {
            _items.AddRange(startingItems.ToDictionary(x => x.BaseInstance.Id, x => x.BaseInstance));
            foreach (var item in startingItems)
            {
                ItemRegistry.Register(item.BaseInstance);
            }
        }

        public bool AddItem(BaseItemInstance instance)
        {
            if (!(_currentWeight + instance.BaseDefinition.Weight <= weightLimit))
            {
                Debug.Log($"Couldn't add {instance.BaseDefinition.ItemName} as it's too heavy, Current weight: {_currentWeight}");
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

            _currentWeight += instance.BaseDefinition.Weight;
            _items.Add(instance.Id, instance);
            return true;
        }

        public bool RemoveItem(BaseItemInstance instance)
        {
            if (!_items.ContainsKey(instance.Id)) return false;
            
            _currentWeight -= instance.BaseDefinition.Weight;
            _items.Remove(instance.Id);

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