using System;
using Items.Equipment;
using Items.Properties;
using UnityEngine;

namespace Items.InstancePropertiesClasses
{
    [Serializable]
    public abstract class ArmourInstanceProperties<T> : InstanceProperties<T>, IDurability where T : ArmourProperties
    {
        [SerializeField] private Durability durability;

        public Durability Durability => durability;
        
        public void InitialiseDurability(Durability newDurability)
        {
            durability = newDurability;
        }
    }
    
    [Serializable]
    public class ArmourInstanceProperties : ArmourInstanceProperties<ArmourProperties>, IDurability, IEquippable
    {
        
    }
}