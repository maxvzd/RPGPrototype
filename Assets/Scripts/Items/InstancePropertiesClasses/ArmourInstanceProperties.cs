using System;
using Items.Equipment;
using Items.Properties;
using UnityEngine;

namespace Items.InstancePropertiesClasses
{
    [Serializable]
    public class ArmourInstanceProperties : InstanceProperties<ArmourProperties>, IDurability, IEquipment
    {
        [SerializeField] private Durability durability;

        public Durability Durability => durability;
        
        public void InitialiseDurability(Durability newDurability)
        {
            durability = newDurability;
        }
    }
}