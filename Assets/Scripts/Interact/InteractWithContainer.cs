using NPC;
using Registries;

namespace Interact
{
    public class InteractWithContainer: Interactable
    {
        public void Awake()
        {
            var container = GetComponent<Container>();
            Id = container.Id;
            InteractRegistry.Register(this);
        }
        
        public override string IconName  => "hand-open";
        
        public override void Interact()
        {
            var items = ContainerRegistry.ByGuid[Id].Inventory;
            
            UiRegistry.ContainerUi.PopulateItems(items, EntitiesRegistry.Player.Inventory);
            UiRegistry.ContainerUi.ShowUI();
        }

    }
}