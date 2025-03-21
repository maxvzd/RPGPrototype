using DataPersistence.SerializableClasses;
using DataPersistence.SerializableClasses.OnObject;
using Items.InstancePropertiesClasses;
using Items.Properties;

namespace Items.Behaviours
{
    public class WeaponBehaviour : ItemBehaviourBase<WeaponProperties, WeaponInstanceProperties>, ISerializableGameObject<SerializableItem>
    {
        public SerializableItem GetSerializable()
        {
            return item.GetSerializable();
        }
    }
}