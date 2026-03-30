using Items;
using Items.ItemDefinitions;
using Items.ItemInstances;
using UnityEngine;

namespace UI.Inventory
{
    public class ItemViewModel
    {
        public string Name { get; }
        public string Description { get; }
        //TODO
        public Texture2D InventoryIcon { get; }
        public float Weight { get; }
        public BaseItemInstance Item { get; }

        public ItemViewModel(BaseItemInstance item)
        {
            Name = item.BaseDefinition.ItemName;
            Description = item.BaseDefinition.Description;
            //InventoryIcon = item.UIProperties.InventoryIcon;
            Weight = item.BaseDefinition.Weight;
            Item = item;
        }
    }
}