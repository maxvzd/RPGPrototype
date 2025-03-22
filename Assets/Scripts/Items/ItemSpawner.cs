using Items.Behaviours;
using Items.InstancePropertiesClasses;
using UnityEngine;

namespace Items
{
    public class ItemSpawner : MonoBehaviour
    {
        public static GameObject SpawnItem(InstanceProperties instance, Vector3 positionToSpawnAt, Quaternion rotation)
        {
            var newItem = Instantiate(instance.BaseItemProperties.Prefab, positionToSpawnAt, rotation);
            var itemBehaviour = newItem.GetComponent<ItemBehaviourBase>();
            itemBehaviour.InitializeInstance(instance);
            return newItem;
        }
    }
}