using Items;
using Items.InstancePropertiesClasses;

namespace Interact.Contexts
{
    public class PickupContext : IInteractionContext
    {
        private readonly Inventory _playerInventorySystem;

        public PickupContext(Inventory playerInventorySystem)
        {
            _playerInventorySystem = playerInventorySystem;
        }

        public bool AddItem(InstanceProperties item)
        {
            return _playerInventorySystem.AddItem(item);
        }
        
        public bool RemoveItem(InstanceProperties item)
        {
            return _playerInventorySystem.RemoveItem(item);
        }
    }
}