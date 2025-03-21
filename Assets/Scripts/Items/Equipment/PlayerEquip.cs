using System.Collections.Generic;
using System.Linq;
using Items.InstancePropertiesClasses;
using UnityEngine;

namespace Items.Equipment
{
    public class PlayerEquip : MonoBehaviour
    {
        private Dictionary<ItemType, IEquipmentSlot> _equipmentSlots = new()
        {
            { ItemType.Weapon, new EquipmentSlot(1) },
            { ItemType.Offhand, new EquipmentSlot(1) },
            { ItemType.Ring, new EquipmentSlot(8) }
        };

        public void ActivateItem(InstanceProperties instance)
        {
            if (UnEquipItem(instance)) return;
            if (instance is not IEquipment) return;

            EquipItem(instance);
        }
        
        public bool UnEquipItem(InstanceProperties instance) 
            => GetEquipmentSlotsForTypes(instance).Any(slot => slot.RemoveItem(instance.InstanceId));
        
        public bool IsItemEquipped(InstanceProperties instance) 
            => GetEquipmentSlotsForTypes(instance).Any(slot => slot.Contains(instance.InstanceId));

        private void EquipItem(InstanceProperties instance)
        {
            var slots = GetEquipmentSlotsForTypes(instance).ToList();
            var slot = slots.FirstOrDefault(x => x.HasEmptySlots());
            if (slot?.AddItem(instance.InstanceId) == true)
            {
                Debug.Log($"Added to empty slot");
                return;
            }
            
            Debug.Log($"Added to existing slot");
            var firstSlot = slots.First();
            firstSlot.RemoveFirstItem();
            firstSlot.AddItem(instance.InstanceId);
        }

        private IEnumerable<IEquipmentSlot> GetEquipmentSlotsForTypes(InstanceProperties instance) 
            => instance.BaseItemProperties.Types.Select(type 
                => _equipmentSlots.TryGetValue(type, out var value) 
                    ? value 
                    : new NullEquipmentSlot());
    }
}