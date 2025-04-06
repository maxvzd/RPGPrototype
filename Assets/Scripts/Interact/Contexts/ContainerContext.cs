using Items;
using UI.Container;

namespace Interact.Contexts
{
    public class ContainerContext : IInteractionContext
    {
        private readonly ContainerUiManager _containerManager;
        private readonly Inventory _playerInventory;
        private readonly Inventory _containerInventory;

        public ContainerContext(ContainerUiManager containerManager, Inventory playerInventory, Inventory containerInventory)
        {
            _containerManager = containerManager;
            _playerInventory = playerInventory;
            _containerInventory = containerInventory;
        }

        public void ShowContainerUI()
        {
            _containerManager.PopulateItems(_playerInventory, _containerInventory);
            _containerManager.ShowUI();
        }
    }
}