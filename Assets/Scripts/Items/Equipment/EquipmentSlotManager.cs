using System;
using System.Collections.Generic;
using System.Linq;
using Items.Equipment.Sheathing;
using Items.ItemInstances;
using UnityEngine;

namespace Items.Equipment
{
    public class EquipmentSlotManager : MonoBehaviour
    {
        private readonly Dictionary<ItemType, IEquipmentSlot> _equipmentSlots = new()
        {
            { ItemType.Weapon, new EquipmentSlot(1) },
            { ItemType.Offhand, new EquipmentSlot(1) },
            { ItemType.Ring, new EquipmentSlot(8) }
        };
        private EquippedMeshManager _equippedMeshManager;
        private WeaponPositionManager _weaponPositionManager;
        
        public IReadOnlyDictionary<ItemType, IEquipmentSlot> EquipmentSlots => _equipmentSlots;
        public bool IsItemEquipped(BaseItemInstance instance)
            => GetEquipmentSlotsForTypes(instance).Any(slot => slot.Contains(instance.Id));

        public bool IsWeaponEquipped => !_equipmentSlots[ItemType.Weapon].HasEmptySlots();
        public bool IsOffHandEquipped => !_equipmentSlots[ItemType.Offhand].HasEmptySlots();
        public EventHandler<ItemType> OnItemEquipped;
        public EventHandler<ItemType> OnItemUnEquipped;
        
        private void Start()
        {
            _equippedMeshManager = GetComponent<EquippedMeshManager>();
            _weaponPositionManager = GetComponent<WeaponPositionManager>();
        }

        public void ToggleItemEquipped(BaseItemInstance instance)
        {
            if (UnEquipItem(instance.Id)) return;
            EquipItem(instance);
        }

        public bool UnEquipItem(Guid id)
        {
            foreach (var (itemType, slot) in _equipmentSlots)
            {
                if (slot.RemoveItem(id))
                {
                    _equippedMeshManager.RemoveGameObject(id);
                    _weaponPositionManager.RemoveItem(id);
            
                    OnItemUnEquipped?.Invoke(this, itemType);
                    return true;
                }
            }
            return false;
        }

        public bool DropItem(BaseItemInstance instance)
        {
            var itemGameObject = _equippedMeshManager.EquippedGameObjects[instance.Id];
            var rotation = itemGameObject.transform.rotation;
            var position = itemGameObject.transform.position;

            DropItem(instance, rotation, position);
           
            return true;
        }
        
        public bool DropItem(BaseItemInstance instance, Quaternion rotation, Vector3 position)
        {
            UnEquipItem(instance.Id);
            ItemSpawner.SpawnItem(instance, position, rotation);
           
            return true;
        }

        private void EquipItem(BaseItemInstance instance)
        {
            if (instance.BaseDefinition is not IEquippable) return;
            
            var validSlots = instance.BaseDefinition.Types
                .Select(itemType => new { ItemType = itemType, Slot = _equipmentSlots[itemType] })
                .Where(entry => entry.Slot != null)
                .ToList();
            
            if (validSlots.Count <= 0) return;
            
            var emptySlot = validSlots.FirstOrDefault(x => x.Slot.HasEmptySlots());
            if (emptySlot is not null)
            {
                AddItemToSlot(emptySlot.ItemType, emptySlot.Slot, instance);
                return;
            }

            var slot = validSlots.FirstOrDefault();
            if(slot is not null) 
            {
                var instanceRemoved = slot.Slot.GetFirstItem();
                UnEquipItem(instanceRemoved);
                AddItemToSlot(slot.ItemType, slot.Slot, instance);
            }
        }

        private void AddItemToSlot(ItemType toSlot, IEquipmentSlot slot, BaseItemInstance instance)
        {
            slot.AddItem(instance.Id);
            if (instance.BaseDefinition is ISheathable sheathable)
            {
                var itemGameObject = _equippedMeshManager.SpawnGameObject(instance);
                _weaponPositionManager.AddItemToSlot(
                    instance.Id,
                    itemGameObject,
                    sheathable.SheathPositions,
                    toSlot);
                OnItemEquipped?.Invoke(this, toSlot);
            }
        }

        private IEnumerable<IEquipmentSlot> GetEquipmentSlotsForTypes(BaseItemInstance instance)
        {
            return instance.BaseDefinition.Types.Select(type
                => _equipmentSlots.TryGetValue(type, out var value)
                    ? value
                    : new EmptyEquipmentSlot());
        }
    }
}