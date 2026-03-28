using System;
using UnityEngine;

namespace Items.ItemDefinitions
{
    [CreateAssetMenu(menuName = "Items/Definitions/Armour")]
    [Serializable]
    public class ArmourDefinition : ItemDefinition
    {
        [SerializeField] private float damageThreshold;
        [SerializeField] private float damageResistance;
        
        public float DamageResistance => damageResistance;
        public float DamageThreshold => damageThreshold;
    }
}