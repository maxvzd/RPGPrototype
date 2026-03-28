using Items.Equipment.Sheathing;
using UnityEngine;

namespace Items.ItemDefinitions
{
    [CreateAssetMenu(menuName = "Items/Definitions/Weapon")]
    public class WeaponDefinition : ItemDefinition, ISheathable
    {
        [SerializeField] private SheathedItemPositions[] sheathPositions;
        public SheathedItemPositions[] SheathPositions => sheathPositions;
    }
}