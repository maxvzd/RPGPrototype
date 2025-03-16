using DataPersistence.SerializableClasses;
using DataPersistence.SerializableClasses.OnObject;
using UnityEngine;

namespace Items
{
    public class ItemBehaviour : MonoBehaviour, ISerializableGameObject<SerializableItem>
    {
        [SerializeField] private ItemProperties item;
        public ItemProperties Properties => item;
        
        public SerializableItem GetSerializable()
        {
            return new SerializableItem(item.ItemName, item.Description, item.Weight);
        }
    }
}