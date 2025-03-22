using System;
using System.Collections.Generic;
using Items.Equipment.Sheathing;
using Items.InstancePropertiesClasses;
using UnityEngine;

namespace Items.Equipment
{
    public class EquippedMeshManager : MonoBehaviour
    {
        [SerializeField] private WeaponPositionManager weaponPositionManager;
        private Dictionary<Guid, GameObject> _equippedInstances = new();

        public void EquipWeapon(InstanceProperties weapon, ItemType toSlot)
        {
            if (weapon.BaseItemProperties is not ISheathable sheathable) return;
        
            var weaponTransform = ItemSpawner.SpawnItem(weapon, Vector3.zero, Quaternion.identity);
            _equippedInstances.Add(weapon.InstanceId, weaponTransform);
            weaponTransform.transform.localScale *= 1f; //have to do this cause I fucked up the scale
            var rb = weaponTransform.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        
            weaponPositionManager.EquipWeapon(
                weapon.InstanceId,
                weaponTransform,
                sheathable.SheathPositions,
                toSlot);
        }

        public bool UnEquipWeapon(Guid instanceId)
        {
            if (!_equippedInstances.Remove(instanceId, out var weaponGameObject)) return false;

            weaponPositionManager.UnEquipWeapon(instanceId);
            Destroy(weaponGameObject);
            return true;
        }
    }
}