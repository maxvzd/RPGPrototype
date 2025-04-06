namespace UI.Container
{
    public class ContainerUiManager : BaseUIManager
    {
        private ContainerController _uiController;
        private Items.Inventory _playerInventory;
        private Items.Inventory _containerInventory;

        private void Start()
        {
            _uiController = new ContainerController(uiDocument.rootVisualElement);
            
            _uiController.PlayerItemClicked += PlayerItemClicked;
            _uiController.ContainerItemClicked += ContainerItemClicked;
            
            HideUI();
        }

        private void ContainerItemClicked(object sender, int e)
        {
            var item = _containerInventory.Items[e];
            if (_containerInventory.RemoveItem(item))
            {
                _playerInventory.AddItem(item);
            }
            RefreshItems();
        }

        private void RefreshItems()
        {
            _uiController.SetItems(_playerInventory, _containerInventory);
        }

        private void PlayerItemClicked(object sender, int e)
        {
            var item = _playerInventory.Items[e];
            if (_playerInventory.RemoveItem(item))
            {
                _containerInventory.AddItem(item);
            }
            RefreshItems();
        }

        protected override void PopulateItems()
        {
            _uiController.PopulateItems(_playerInventory, _containerInventory);
        }

        public void PopulateItems(Items.Inventory playerInventory, Items.Inventory containerInventory)
        {
            _playerInventory = playerInventory;
            _containerInventory = containerInventory;
        }
    }
}
