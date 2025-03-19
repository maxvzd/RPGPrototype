using System;
using Items;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace UI.Inventory
{
    public class InventoryUIManager : MonoBehaviour
    {
        [SerializeField] private UIDocument inventoryUI;
        private InventoryController _inventoryController;
        private bool _uiIsHidden;

        public EventHandler UiShown;
        public EventHandler UiHidden;
        private global::Inventory _inventory;

        private void Start()
        {
            _inventoryController = new InventoryController(inventoryUI.rootVisualElement);
            
            _inventory = GetComponent<global::Inventory>();
            HideUI();
        }

        private void HideUI()
        {
            if (_uiIsHidden) return;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inventoryUI.rootVisualElement.style.display = DisplayStyle.None;
            _uiIsHidden = true;

            UiHidden?.Invoke(this, EventArgs.Empty);
        }

        private void ShowUI()
        {
            if (!_uiIsHidden) return;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            _inventoryController.PopulateItems(_inventory.Items);

            inventoryUI.rootVisualElement.style.display = DisplayStyle.Flex;
            _uiIsHidden = false;
            UiShown?.Invoke(this, EventArgs.Empty);
        }

        public bool ToggleUI()
        {
            if (_uiIsHidden)
            {
                ShowUI();
                Debug.Log("Show");
            }
            else
            {
                HideUI();
                Debug.Log("Hide");
            }

            return !_uiIsHidden;
        }

        public void DropSelectedItem()
        {
            var item = _inventoryController.CurrentlyHoveredItem;
            if (item is null) return;
            
            _inventory.RemoveItem(item);
            _inventoryController.PopulateItems(_inventory.Items);
        }
    }
}