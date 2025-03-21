using System;
using DataPersistence.SerializableClasses;
using DataPersistence.SerializableClasses.OnObject;
using Items.InstancePropertiesClasses;
using Items.Properties;
using UnityEngine;

namespace Items.Behaviours
{
    [Serializable]
    public class ItemBase<T1, T2> : ISerializableGameObject<SerializableItem> where T1 : ItemProperties where T2 : InstanceProperties<T1>
    {
        [SerializeField] private T2 instanceProperties;
        
        public T2 InstanceProperties => instanceProperties;

        public void SetItemInstanceProperties(T2 instance)
        {
            if (instanceProperties.IsInstanceTypeInitialised) return;
            instanceProperties = instance;
        }

        public SerializableItem GetSerializable()
        {
            return new SerializableItem(
                instanceProperties.BaseItemProperties.ItemName, 
                instanceProperties.BaseItemProperties.Description, 
                instanceProperties.BaseItemProperties.Weight);
        }
    }
}