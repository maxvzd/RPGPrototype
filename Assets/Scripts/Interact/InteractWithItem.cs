using System;
using Interact.Contexts;
using Items;
using UnityEngine;

namespace Interact
{
    public class InteractWithItem : MonoBehaviour, IInteract<PickupContext>
    {
        private ItemProperties _itemProps;

        public void Start()
        {
            var itemBehaviour = GetComponent<ItemBehaviour>();
            _itemProps = itemBehaviour.Properties;
        }
        
        private void Interact(PickupContext interactionContext)
        {
            if (interactionContext.AddItem(_itemProps))
            {
                Destroy(gameObject); 
            }
        }

        public Type GetInteractionType() => typeof(PickupContext);
        
        public void Interact(IInteractionContext context)
        {
            Interact(context as PickupContext);
        }
    }
}