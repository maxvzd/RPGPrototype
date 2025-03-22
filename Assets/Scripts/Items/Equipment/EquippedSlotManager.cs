using System;
using System.Collections.Generic;
using System.Linq;
using Items.Equipment.Sheathing;
using Items.InstancePropertiesClasses;
using UnityEngine;

namespace Items.Equipment
{
    public class EquippedSlotManager : MonoBehaviour
    {
        private readonly Dictionary<ItemType, IEquipmentSlot> _equipmentSlots = new()
        {
            { ItemType.Weapon, new EquipmentSlot(1) },
            { ItemType.Offhand, new EquipmentSlot(1) },
            { ItemType.Ring, new EquipmentSlot(8) }
        };

        private EquippedMeshManager _equippedMeshManager;
        
        public bool IsItemEquipped(InstanceProperties instance)
            => GetEquipmentSlotsForTypes(instance).Any(slot => slot.Contains(instance.InstanceId));

        public void ActivateItem(InstanceProperties instance)
        {
            if (UnEquipItem(instance.InstanceId)) return;

            EquipItem(instance);
        }

        public bool UnEquipItem(Guid id)
        {
            if (!_equipmentSlots.Any(slot => slot.Value.RemoveItem(id))) return false;
            
            _equippedMeshManager.UnEquipWeapon(id);
            return true;
        }
        
        private void Start()
        {
            _equippedMeshManager = GetComponent<EquippedMeshManager>();
        }

        private void EquipItem(InstanceProperties instance)
        {
            if (instance.BaseItemProperties is not IEquippable) return;
            
            var validSlots = instance.BaseItemProperties.Types
                .Select(itemType => new { ItemType = itemType, Slot = _equipmentSlots.GetValueOrDefault(itemType) })
                .Where(entry => entry.Slot != null)
                .ToList();
            
            if (validSlots.Count <= 0) return;
            
            foreach (var slotEntry in validSlots.Where(x => x.Slot.HasEmptySlots()))
            {
                AddItemToSlot(slotEntry.ItemType, slotEntry.Slot, instance);
                return;
            }

            foreach (var slotEntry in validSlots)
            {
                var instanceRemoved = slotEntry.Slot.GetFirstItem();
                UnEquipItem(instanceRemoved);
                
                AddItemToSlot(slotEntry.ItemType, slotEntry.Slot, instance);
                return;
            }
        }

        private void AddItemToSlot(ItemType slotType, IEquipmentSlot slot, InstanceProperties instance)
        {
            slot.AddItem(instance.InstanceId);
            if (instance.BaseItemProperties is ISheathable)
            {
                _equippedMeshManager.EquipWeapon(instance, slotType);
            }
        }

        private IEnumerable<IEquipmentSlot> GetEquipmentSlotsForTypes(InstanceProperties instance)
        {
            return instance.BaseItemProperties.Types.Select(type
                => _equipmentSlots.TryGetValue(type, out var value)
                    ? value
                    : new NullEquipmentSlot());
        }
    }
}