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
            if (UnEquipItem(instance.InstanceId))
            {
                
            }
            else
            {
                if (instance is not IEquipment) return;
                EquipItem(instance.InstanceId);
            }
        }

        private bool EquipItem(Guid guid)
        {
            return _equippedItems.Add(guid);
        }

        public bool UnEquipItem(Guid guid)
        {
            return _equippedItems.Contains(guid) && _equippedItems.Remove(guid);
        }
    }
}
