using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using Items.ItemInstances;
using UnityEngine.UIElements;

namespace UI.Inventory
{
    public class InventoryController
    {
        public BaseItemInstance CurrentlyHoveredBaseItem => _listController.CurrentlyHoveredIndex > -1 ? _items[_listController.CurrentlyHoveredIndex] : null;

        private readonly InventoryListController _listController;
        private IReadOnlyList<BaseItemInstance> _items;

        public EventHandler<BaseItemInstance> ItemClicked;
        
        public InventoryController(VisualElement root)
        {
            var inventoryListView = root.Q<MultiColumnListView>(InventoryUIConstants.InventoryItems);
            _listController = new InventoryListController(inventoryListView);
            _listController.ItemClicked += (sender, i) =>
            {
                var item = _listController.GetItemAtIndex(i);
                ItemClicked?.Invoke(sender, item);
            };
        }

        public void PopulateItems(IEnumerable<BaseItemInstance> items)
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