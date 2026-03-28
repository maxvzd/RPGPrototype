using Items.Equipment.Sheathing;
using UnityEngine;

namespace Items.ItemDefinitions
{
    [CreateAssetMenu(menuName = "Items/Definitions/Sheathable Armour")]
    public class SheathableArmourDefinition : ArmourDefinition, ISheathable
    {
        [SerializeField] private SheathedItemPositions[] sheathPositions;
        public SheathedItemPositions[] SheathPositions => sheathPositions;
    }
}