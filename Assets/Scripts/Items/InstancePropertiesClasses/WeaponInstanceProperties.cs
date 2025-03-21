using System;
using Items.Equipment;
using Items.Properties;
using UnityEngine;

namespace Items.InstancePropertiesClasses
{
    [Serializable]
    public class WeaponInstanceProperties : InstanceProperties<WeaponProperties>, IEquipment
    {
        [SerializeField] private float durability = 100f;
        public float Durability => durability;

        public void Degrade()
        {
            durability -= 1;
        }
        
    }
}