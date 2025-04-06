using System.Collections.Generic;
using Items;
using Items.Equipment;
using UnityEngine;

namespace UI.Inventory
{
    public class PlayerInventoryUiManager : BaseUIManager
    {
        private InventoryController _inventoryController;
        private global::Items.Inventory _inventory;
        private EquippedSlotManager _playerEquipped;

        private void Start()
        {
            _inventoryController = new InventoryController(uiDocument.rootVisualElement);
            _inventoryController.ItemClicked += OnItemClicked;
            
            _inventory = GetComponent<global::Items.Inventory>();
            _playerEquipped = GetComponent<EquippedSlotManager>();
            HideUI();
        }

        private void OnItemClicked(object sender, int e)
        {
            _playerEquipped.ActivateItem(_inventory.Items[e]);

            SetSelectedIndices();
        }

        private void SetSelectedIndices()
        {
            var selectedIndices = new List<int>();
            for (var i = 0; i < _inventory.Items.Count; i++)
            {
                var item = _inventory.Items[i];
                if (_playerEquipped.IsItemEquipped(item))
                {
                    selectedIndices.Add(i);
                }
            }
            _inventoryController.SetSelectedItems(selectedIndices);
        }

        public void DropSelectedItem()
        {
            var instance = _inventoryController.CurrentlyHoveredItem;
            if (instance is null) return;

            if (_inventory.RemoveItem(instance))
            {
                if (_playerEquipped.UnEquipItem(instance.InstanceId))
                {
                    SetSelectedIndices();
                }
                var currTransform = transform;
                var positionToSpawnAt = currTransform.position + currTransform.forward * 0.5f + currTransform.up * 1.5f;
                ItemSpawner.SpawnItem(instance, positionToSpawnAt, Quaternion.identity);
            }
            
            _inventoryController.PopulateItems(_inventory.Items);
            _inventoryController.ResetCurrentlyHovered();
        }

        protected override void PopulateItems()
        {
            _inventoryController.PopulateItems(_inventory.Items);
        }
    }
}