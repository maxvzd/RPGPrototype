using System;
using System.Collections.Generic;
using Items.Equipment;
using Items.ItemInstances;
using UnityEngine;

namespace Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private float weightLimit = 500;
        private float _currentWeight = 0;
        private readonly List<BaseItemInstance> _items = new();
        private EquippedSlotManager _equipment;

        public IReadOnlyList<BaseItemInstance> Items => _items;

        private void Start()
        {
            _equipment = GetComponent<EquippedSlotManager>();
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
            _items.Add(instance);
            return true;
        }

        public bool RemoveItem(BaseItemInstance instance)
        {
            if (!_items.Exists(x => x == instance)) return false;

            if (_equipment is not null)
            {
                if (_equipment.IsItemEquipped(instance))
                {
                    _equipment.UnEquipItem(instance.Id);
                }
            }
            
            _currentWeight -= instance.BaseDefinition.Weight;
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