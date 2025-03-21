using Items.InstancePropertiesClasses;
using Items.Properties;
using UnityEngine;

namespace Items.Behaviours
{
    public abstract class ItemBehaviourBase : MonoBehaviour
    {
        public abstract ItemProperties GetBaseProperties();
        public abstract InstanceProperties GetBaseInstance();
        public abstract void InitializeInstance(InstanceProperties instance);
    }
    
    public abstract class ReadOnlyItemBehaviourBase<T1, T2> : ItemBehaviourBase
        where T1 : ItemProperties
        where T2 : InstanceProperties<T1>
    {
        [SerializeField] protected ItemBase<T1, T2> item;

        public T1 ItemProperties => item.ItemProperties;
        public T2 ItemInstanceProperties => item.InstanceProperties;
        
        public override ItemProperties GetBaseProperties() => ItemProperties;
        public override InstanceProperties GetBaseInstance() => ItemInstanceProperties;

        public override void InitializeInstance(InstanceProperties instance)
        {
            if (instance is T2 concreteInstance)
            {
                item.SetItemInstanceProperties(concreteInstance);
            }
        }
    }

    public abstract class ItemBehaviourBase<T1, T2> : ReadOnlyItemBehaviourBase<T1, T2>
        where T1 : ItemProperties
        where T2 : InstanceProperties<T1>
    {
        public void InitializeInstance(T2 instance)
        {
            item.SetItemInstanceProperties(instance);
        }
        
        private void Start()
        {
            ItemInstanceProperties.SetItemProperties(ItemProperties);
        }
    }
}