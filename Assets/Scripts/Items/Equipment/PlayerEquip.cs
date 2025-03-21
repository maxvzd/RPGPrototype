using System;
using System.Collections.Generic;
using Helpers;
using Items.InstancePropertiesClasses;
using UnityEngine;

namespace Items.Equipment
{
    public class PlayerEquip : MonoBehaviour
    {
        private readonly HashSet<Guid> _equippedItems = new();
        public ReadonlyHashSet<Guid> EquippedItems => new(_equippedItems);

        public void ActivateItem(InstanceProperties instance)
        {
            if (_equippedItems.Contains(instance.InstanceId))
            {
                _equippedItems.Remove(instance.InstanceId);
            }
            else
            {
                if (instance is not IEquipment) return;
                _equippedItems.Add(instance.InstanceId);
            }
        }
    }
}
