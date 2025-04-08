using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using Items.InstancePropertiesClasses;
using UnityEngine.UIElements;

namespace UI.Inventory
{
    public class InventoryController
    {
        public InstanceProperties CurrentlyHoveredItem => _listController.CurrentlyHoveredIndex > -1 ? _items[_listController.CurrentlyHoveredIndex] : null;

        private readonly InventoryListController _listController;
        private IReadOnlyList<InstanceProperties> _items;

        public EventHandler<int> ItemClicked;
        
        public InventoryController(VisualElement root)
        {
            var inventoryListView = root.Q<MultiColumnListView>(InventoryUIConstants.InventoryItems);
            _listController = new InventoryListController(inventoryListView);
            _listController.ItemClicked += (sender, i) =>
            {
                ItemClicked?.Invoke(sender, i);
            };
        }

        public void PopulateItems(IEnumerable<InstanceProperties> items)
        {
            _items = items.ToList();
            _listController.SetupInventoryList(_items);
        }

        public void ResetCurrentlyHovered()
        {
            _listController.ResetCurrentlyHovered();
        }

        public void SetSelectedItems(IEnumerable<int> selectedIndices)
        {
            _listController.SetSelectedItems(selectedIndices);
        }
    }
}