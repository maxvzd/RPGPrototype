using System.Collections.Generic;
using System.Linq;
using Constants;
using Items;
using UnityEngine.UIElements;

namespace UI.Inventory
{
    public class InventoryController
    {
        public ItemProperties CurrentlyHoveredItem => _listController.CurrentlyHoveredIndex > -1 ? _items[_listController.CurrentlyHoveredIndex] : null;
        
        private readonly InventoryListController _listController;
        private IReadOnlyList<ItemProperties> _items;

        public InventoryController(VisualElement root)
        {
            var inventoryListView = root.Q<MultiColumnListView>(InventoryUIConstants.InventoryItems);
             _listController = new InventoryListController(inventoryListView);
        }

        public void PopulateItems(IEnumerable<ItemProperties> items)
        {
            _items = items.ToList();
            _listController.PopulateInventoryList(_items);
        }
    }
}