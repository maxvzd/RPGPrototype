using Interact.Contexts;
using Items;
using UI.Container;
using UnityEngine;

namespace Interact.ContextBuilders
{
    public class ContainerContextBuilder : IContextBuilder
    {
        public ContainerContext Build(ContainerUiManager containerUi, Inventory playerInventory, GameObject containerGameObject)
        {
            var containerInventory = containerGameObject.GetComponent<Inventory>();
            return new ContainerContext(containerUi, playerInventory, containerInventory);
        }
    }
}