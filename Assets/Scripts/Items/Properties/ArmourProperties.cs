using System;
using UnityEngine;

namespace Items.Properties
{
    [CreateAssetMenu(menuName = "Items/Armour")]
    [Serializable]
    public class ArmourProperties : ItemProperties
    {
        [SerializeField] private float damageThreshold;
        [SerializeField] private float damageResistance;
        
        public float DamageResistance => damageResistance;
        public float DamageThreshold => damageThreshold;
    }
}