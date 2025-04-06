using Interact.ContextBuilders;
using Interact.Contexts;
using UnityEngine;

namespace Interact
{
    public class InteractWithContainer: MonoBehaviour, IInteract<ContainerContextBuilder>
    {
        public ContainerContextBuilder GetInteractionContext()
        {
            return new ContainerContextBuilder();
        }

        public void Interact(IInteractionContext context)
        {
            if (context is ContainerContext containerContext)
            {
                containerContext.ShowContainerUI();
            }
        }

        public string IconName  => "hand-open";
    }
}