using Items.Behaviours;
using Items.ItemInstances;
using UnityEngine;

namespace Items
{
    public class ItemSpawner : MonoBehaviour
    {
        public static GameObject SpawnItem(BaseItemInstance instance, Vector3 positionToSpawnAt, Quaternion rotation)
        {
            var newItem = Instantiate(instance.BaseDefinition.Prefab, positionToSpawnAt, rotation);
            var itemBehaviour = newItem.GetComponent<BaseItemBehaviour>();
            itemBehaviour.SetInstance(instance);
            return newItem;
        }
    }
}