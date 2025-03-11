using DataPersistence.SerializableClasses;
using DataPersistence.SerializableClasses.OnObject;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour, ISerializableGameObject<SerializableItem>
    {
        [SerializeField] private float weight;
        [SerializeField] private string itemName;
        [SerializeField] private string description;
        
        public SerializableItem GetSerializable()
        {
            return new SerializableItem(itemName, description, weight);
        }
    }
}