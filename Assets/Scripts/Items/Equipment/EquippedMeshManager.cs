using System;
using System.Collections.Generic;
using Constants;
using Items.ItemInstances;
using UnityEngine;

namespace Items.Equipment
{
    public class EquippedMeshManager : MonoBehaviour
    {
        private Dictionary<Guid, GameObject> _equippedGameObjects = new();
        public IReadOnlyDictionary<Guid, GameObject> EquippedGameObjects => _equippedGameObjects;

        public GameObject SpawnGameObject(BaseItemInstance weapon)
        {
            var weaponTransform = ItemSpawner.SpawnItem(weapon, Vector3.zero, Quaternion.identity);
            _equippedGameObjects.Add(weapon.Id, weaponTransform);
            weaponTransform.transform.localScale *= 1f; //have to do this cause I fucked up the scale
            var rb = weaponTransform.GetComponent<Rigidbody>();
            var c = weaponTransform.GetComponent<Collider>();
            rb.isKinematic = true;
            rb.excludeLayers = LayerMask.GetMask(LayerConstants.Player);
            c.excludeLayers = LayerMask.GetMask(LayerConstants.Player);
            return weaponTransform;
        }

        public bool RemoveGameObject(Guid instanceId)
        {
            if (!_equippedGameObjects.Remove(instanceId, out var weaponGameObject)) return false;
            Destroy(weaponGameObject);
            return true;
        }
    }
}