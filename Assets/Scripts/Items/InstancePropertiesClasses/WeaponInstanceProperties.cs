using System;
using Items.Properties;
using UnityEngine;

namespace Items.InstancePropertiesClasses
{
    [Serializable]
    public class WeaponInstanceProperties : InstanceProperties<WeaponProperties>
    {
        [SerializeField] private float durability = 100f;
        public float Durability => durability;

        public void Degrade()
        {
            durability -= 1;
        }
        
    }
}