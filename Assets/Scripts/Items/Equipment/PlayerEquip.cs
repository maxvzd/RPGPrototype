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
                UnEquipItem(instance.InstanceId);
            }
            else
            {
                if (instance is not IEquipment) return;
                EquipItem(instance.InstanceId);
            }
        }

        private void EquipItem(Guid guid)
        {
            _equippedItems.Add(guid);
        }

        private void UnEquipItem(Guid guid)
        {
            _equippedItems.Remove(guid);
        }
    }
}
