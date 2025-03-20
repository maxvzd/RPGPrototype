using DataPersistence.SerializableClasses;
using DataPersistence.SerializableClasses.OnObject;
using UnityEngine;

namespace Items
{
    public class ItemBehaviour : MonoBehaviour, ISerializableGameObject<SerializableItem>
    {
        [SerializeField] private ItemProperties itemProperties;
        [SerializeField] private ItemInstanceProperties instanceProperties;

        public ItemInstanceProperties InstanceProperties => instanceProperties;
        public ItemProperties ItemProperties => itemProperties;

        public void InitializeInstance(ItemInstanceProperties instance)
        {
            if (instanceProperties.IsInitialisedSpecialProp) return;
            instanceProperties = instance;
        }

        public SerializableItem GetSerializable()
        {
            return new SerializableItem(itemProperties.ItemName, itemProperties.Description, itemProperties.Weight);
        }

        private void Start()
        {
            if (instanceProperties is null || instanceProperties.Item is not null) return;
            InstanceProperties.Initialise(ItemProperties);
        }
    }
}