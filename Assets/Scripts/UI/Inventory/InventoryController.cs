using System.Collections.Generic;
using Constants;
using Items;
using UnityEngine.UIElements;

namespace UI.Inventory
{
    public class InventoryController
    {
        private readonly InventoryListController _listController;


        public InventoryController(VisualElement root)
        {
            var inventoryListView = root.Q<MultiColumnListView>(InventoryUIConstants.InventoryItems);
             _listController = new InventoryListController(inventoryListView);
             
             
             //_listController.ItemChanged += ListControllerOnItemChanged;
            //
            // InventoryTabController tabController = new InventoryTabController(root);
            // tabController.TabSelected += TabControllerOnTabSelected;
            //
            // _itemInfoPanelController = new InventoryItemInfoPanelController(root);
            // _itemInfoPanelController.RetrieveItemButtonClicked += RetrieveItemButtonClicked;
            //
            // _equipmentPanelController = new EquipmentPanelController(root);
            // _equipmentPanelController.ItemUnequipped += ItemUnequipped;
            //
            // _items = new Dictionary<Guid, IItem>();
        }

        public void PopulateItems(IEnumerable<ItemProperties> items)
        {
            _listController.PopulateInventoryList(items);
        }
    }
}