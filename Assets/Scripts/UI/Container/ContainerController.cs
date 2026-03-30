using System;
using Constants;
using Items.ItemInstances;
using UI.Inventory;
using UnityEngine.UIElements;

namespace UI.Container
{
    public class ContainerController
    {
        private readonly InventoryListController _containerListController;
        private readonly InventoryListController _playerListController;
        
        public EventHandler<BaseItemInstance> PlayerItemClicked;
        public EventHandler<BaseItemInstance> ContainerItemClicked;

        public ContainerController(VisualElement root)
        {
            var playerListView = root.Q<MultiColumnListView>(ContainerUIConstants.PlayerInventory);
            var containerListView = root.Q<MultiColumnListView>(ContainerUIConstants.ContainerInventory);
            
            _playerListController = new InventoryListController(playerListView);
            _containerListController = new InventoryListController(containerListView);
            
            _playerListController.ItemClicked += (sender, i) =>
            {
                var item = _playerListController.GetItemAtIndex(i);
                PlayerItemClicked?.Invoke(sender, item);
            };
            
            _containerListController.ItemClicked += (sender, i) =>
            {
                var item = _containerListController.GetItemAtIndex(i);
                ContainerItemClicked?.Invoke(sender, item);
            };
        }
        
        public void PopulateItems(Items.Inventory playerInventory, Items.Inventory containerInventory)
        {
            _playerListController.SetupInventoryList(playerInventory.Items.Values);
            _containerListController.SetupInventoryList(containerInventory.Items.Values);
        }

        public void SetItems(Items.Inventory playerInventory, Items.Inventory containerInventory)
        {
            _playerListController.SetItems(playerInventory.Items.Values);
            _containerListController.SetItems(containerInventory.Items.Values);
        }
    }
}