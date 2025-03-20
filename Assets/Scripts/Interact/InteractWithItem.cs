using Interact;
using Interact.Contexts;
using Items;
using UnityEngine;

namespace Interact
{
    public class InteractWithItem : MonoBehaviour, IInteract<PickupContextBuilder>
    {
        public string IconName => "hand-open";
        
        private ItemInstanceProperties _itemProps;

        public void Start()
        {
            var itemBehaviour = GetComponent<ItemBehaviour>();
            _itemProps = itemBehaviour.InstanceProperties;
        }

        public PickupContextBuilder GetInteractionContext()
        {
            return new PickupContextBuilder();
        }

        public void Interact(IInteractionContext context)
        {
            if (context is PickupContext pickupContext)
            {
                Interact(pickupContext);
            }
        }
        
        private void Interact(PickupContext interactionContext)
        {
            if (interactionContext.AddItem(_itemProps))
            {
                Destroy(gameObject); 
            }
        }
    }
}

public class PickupContextBuilder : IContextBuilder
{
    public PickupContext AddInventoryContext(Inventory inventory)
    {
        return new PickupContext(inventory);
    }
}