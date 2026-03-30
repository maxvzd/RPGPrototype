using System;
using System.Collections.Generic;
using FirstPerson;
using Items;
using Items.Equipment;
using Items.Equipment.Sheathing;
using Items.ItemInstances;
using NPC;
using Registries;
using UnityEngine;

namespace UI.Inventory
{
    public class PlayerInventoryUiManager : BaseUIManager
    {
        private InventoryController _inventoryController;
        private global::Items.Inventory _inventory;
        private EquipmentSlotManager _playerEquipment;
        private FirstPersonCombatAnimationManager _firstPersonAnimationController;
        private WeaponSheathing _weaponSheathing;

        private void Awake()
        {
            UiRegistry.Register(this);
        }

        private void Start()
        {
            _inventoryController = new InventoryController(uiDocument.rootVisualElement);
            _inventoryController.ItemClicked += OnItemClicked;

            _inventory = GetComponent<global::Items.Inventory>();
            _playerEquipment = GetComponent<EquipmentSlotManager>();
            _firstPersonAnimationController = GetComponent<FirstPersonCombatAnimationManager>();
            _weaponSheathing = GetComponent<WeaponSheathing>();
            HideUI();
            
            var animationEventHandler = GetComponent<PlayerAnimationEventListener>();
            animationEventHandler.WeaponDropped += WeaponDropped;
            animationEventHandler.OffHandDropped += OffHandDropped;
        }

        private void OnItemClicked(object sender, BaseItemInstance e)
        {
            _playerEquipment.ToggleItemEquipped(_inventory.Items[e.Id]);
            SetSelectedIndices();
        }

        private void SetSelectedIndices()
        {
            var selectedIndices = new List<int>();

            var i = 0;
            foreach (var item in _inventory.Items.Values)
            {
                if (_playerEquipment.IsItemEquipped(item))
                {
                    selectedIndices.Add(i);
                }
                i++;
            }
            _inventoryController.SetSelectedItems(selectedIndices);
        }

        public void DropSelectedItem()
        {
            var instance = _inventoryController.CurrentlyHoveredBaseItem;
            if (instance is null) return;

            if (_inventory.RemoveItem(instance))
            {
                var equipmentSlots = EntitiesRegistry.Player.EquipmentSlots;
                
                if (equipmentSlots.EquipmentSlots[ItemType.Weapon].Contains(instance.Id) && !_weaponSheathing.IsWeaponSheathed)
                {
                    _firstPersonAnimationController.MoveToDropItemState(FirstPersonAnimationLayers.RightArm);
                }
                else if (equipmentSlots.EquipmentSlots[ItemType.Offhand].Contains(instance.Id) && !_weaponSheathing.IsOffhandSheathed)
                {
                    _firstPersonAnimationController.MoveToDropItemState(FirstPersonAnimationLayers.LeftArm);
                }
                else
                {
                    var positionToSpawnAt = transform.position + transform.forward * 0.5f + transform.up * 1.5f;
                    equipmentSlots.DropItem(instance, Quaternion.identity, positionToSpawnAt);
                }
                SetSelectedIndices();
            }

            _inventoryController.PopulateItems(_inventory.Items.Values);
            _inventoryController.ResetCurrentlyHovered();
        }

        protected override void PopulateItems()
        {
            _inventoryController.PopulateItems(_inventory.Items.Values);
            SetSelectedIndices();
        }
        
        private void OffHandDropped(object sender, EventArgs e)
        {
            DropItem(ItemType.Offhand);
        }

        private void WeaponDropped(object sender, EventArgs e)
        {
            DropItem(ItemType.Weapon);
        }

        private static void DropItem(ItemType type)
        {
            var equipmentSlots = EntitiesRegistry.Player.EquipmentSlots;
            var itemId = equipmentSlots.EquipmentSlots[type].GetFirstItem();
            var instance = ItemRegistry.ByGuid[itemId];
            equipmentSlots.DropItem(instance);
        }
    }
}