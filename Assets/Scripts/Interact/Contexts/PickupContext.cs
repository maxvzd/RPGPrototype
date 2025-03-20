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

        public bool AddItem(ItemInstanceProperties item)
        {
            return _playerInventorySystem.AddItem(item);
        }
        
        public bool RemoveItem(ItemInstanceProperties item)
        {
            return _playerInventorySystem.RemoveItem(item);
        }
    }
}