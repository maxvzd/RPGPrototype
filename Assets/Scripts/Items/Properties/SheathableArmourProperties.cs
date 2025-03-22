using Items.Equipment.Sheathing;
using UnityEngine;

namespace Items.Properties
{
    [CreateAssetMenu(menuName = "Items/Sheathable Armour")]
    public class SheathableArmourProperties : ArmourProperties, ISheathable
    {
        [SerializeField] private SheathedItemPositions[] sheathPositions;
        public SheathedItemPositions[] SheathPositions => sheathPositions;
    }
}