using System;
using Items.ItemDefinitions;
using UnityEngine;

namespace Items.ItemInstances
{
    [Serializable]
    public class ArmourInstance : BaseItemInstance<ArmourDefinition>, IDurability
    {
        public void InitialiseDurability(Durability durabilityToSet)
        {
            durability = durabilityToSet;
        }

        [SerializeField] private Durability durability;
        public Durability Durability => durability;
    }
}