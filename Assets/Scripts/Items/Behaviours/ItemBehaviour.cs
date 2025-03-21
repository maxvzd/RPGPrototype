using DataPersistence.SerializableClasses;
using DataPersistence.SerializableClasses.OnObject;
using Items.InstancePropertiesClasses;
using Items.Properties;

namespace Items.Behaviours
{
    public class ItemBehaviour : ItemBehaviourBase<ItemProperties, ItemInstanceProperties>, ISerializableGameObject<SerializableItem>
    {
        public SerializableItem GetSerializable()
        {
            return item.GetSerializable();
        }
        
        private void Start()
        {
            ItemInstanceProperties.SetItemProperties(ItemProperties);
        }
    }
}