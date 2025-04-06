using System;
using System.Collections.Generic;
using Constants;
using Items.InstancePropertiesClasses;
using UI.Inventory;
using UnityEngine.UIElements;

namespace UI.Container
{
    public class ContainerController
    {
        private readonly InventoryListController _containerListController;
        private readonly InventoryListController _playerListController;
        
        public EventHandler<int> PlayerItemClicked;
        public EventHandler<int> ContainerItemClicked;

        public ContainerController(VisualElement root)
        {
            var playerListView = root.Q<MultiColumnListView>(ContainerUIConstants.PlayerInventory);
            var containerListView = root.Q<MultiColumnListView>(ContainerUIConstants.ContainerInventory);
            
            _playerListController = new InventoryListController(playerListView);
            _containerListController = new InventoryListController(containerListView);
            
            _playerListController.ItemClicked += (sender, i) =>
            {
                PlayerItemClicked?.Invoke(sender, i);
            };
            
            _containerListController.ItemClicked += (sender, i) =>
            {
                ContainerItemClicked?.Invoke(sender, i);
            };
        }
        
        public void PopulateItems(Items.Inventory playerInventory, Items.Inventory containerInventory)
        {
            _playerListController.SetupInventoryList(playerInventory.Items);
            _containerListController.SetupInventoryList(containerInventory.Items);
        }

        public void SetItems(Items.Inventory playerInventory, Items.Inventory containerInventory)
        {
            _playerListController.SetItems(playerInventory.Items);
            _containerListController.SetItems(containerInventory.Items);
        }
    }
}