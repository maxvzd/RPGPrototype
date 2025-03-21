using Items;
using Items.Properties;
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

        public ItemViewModel(ItemProperties item)
        {
            Name = item.ItemName;
            Description = item.Description;
            //InventoryIcon = item.UIProperties.InventoryIcon;
            Weight = item.Weight;
        }
    }
}