using Items;

namespace Interact.Contexts
{
    public class PickupContext : IInteractionContext
    {
        private readonly Inventory _playerInventorySystem;

        public PickupContext(Inventory playerInventorySystem)
        {
            _playerInventorySystem = playerInventorySystem;
        }

        public bool AddItem(ItemProperties item)
        {
            return _playerInventorySystem.AddItem(item);
        }
        
        public bool RemoveItem(ItemProperties item)
        {
            return _playerInventorySystem.RemoveItem(item);
        }
    }
}