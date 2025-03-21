using Items.Behaviours;
using Items.InstancePropertiesClasses;
using UnityEngine;

namespace Items
{
    public class ItemSpawner : MonoBehaviour
    {
        public static void SpawnItem(InstanceProperties instance, Vector3 positionToSpawnAt, Quaternion rotation)
        {
            var newItem = Instantiate(instance.Item.Prefab, positionToSpawnAt, rotation);
            var itemBehaviour = newItem.GetComponent<ItemBehaviourBase>();
            itemBehaviour.InitializeInstance(instance);
        }
    }
}