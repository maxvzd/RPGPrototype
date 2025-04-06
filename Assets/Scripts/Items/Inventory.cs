using System;
using System.Collections.Generic;
using Items.Equipment;
using Items.InstancePropertiesClasses;
using UnityEngine;

namespace Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private float weightLimit = 500;
        private float _currentWeight = 0;
        private readonly List<InstanceProperties> _items = new();
        private EquippedSlotManager _equipment;

        public IReadOnlyList<InstanceProperties> Items => _items;

        private void Start()
        {
            _equipment = GetComponent<EquippedSlotManager>();
        }

        public bool AddItem(InstanceProperties instance)
        {
            if (!(_currentWeight + instance.BaseItemProperties.Weight <= weightLimit))
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

            if (_equipment is not null)
            {
                if (_equipment.IsItemEquipped(instance))
                {
                    _equipment.UnEquipItem(instance.InstanceId);
                }
            }
            
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