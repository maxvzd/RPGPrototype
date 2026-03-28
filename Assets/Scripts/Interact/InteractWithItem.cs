using Items.Behaviours;
using Items.ItemInstances;
using NPC;
using Registries;

namespace Interact
{
    public class InteractWithItem : Interactable
    {
        private BaseItemInstance _baseItemProps;
        public override string IconName => "hand-open";
        
        public void Awake()
        {
            var itemBehaviour = GetComponent<BaseItemBehaviour>();
            _baseItemProps = itemBehaviour.Instance;
            Id = _baseItemProps.Id;
            InteractRegistry.Register(this);
        }
        
        public override void Interact()
        {
            if (EntitiesRegistry.Player.Inventory.AddItem(_baseItemProps))
            {
                Destroy(gameObject); 
            }
        }
    }
}