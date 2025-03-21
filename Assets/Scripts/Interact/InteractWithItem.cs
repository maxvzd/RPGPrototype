using Interact;
using Interact.Contexts;
using Items;
using Items.Behaviours;
using Items.InstancePropertiesClasses;
using UnityEngine;

namespace Interact
{
    public class InteractWithItem : MonoBehaviour, IInteract<PickupContextBuilder>
    {
        private  InstanceProperties _itemProps;
        
        public string IconName => "hand-open";

        public void Start()
        {
            var itemBehaviour = GetComponent<ItemBehaviourBase>();
            _itemProps = itemBehaviour.GetBaseInstance();
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