using System;
using Items.Properties;
using UnityEngine;

namespace Items.InstancePropertiesClasses
{
    public abstract class InstanceProperties
    {
        public abstract bool IsInstanceTypeInitialised { get; }
        public abstract ItemProperties BaseItemProperties { get; }
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
    
    [Serializable]
    public abstract class InstanceProperties<T> : InstanceProperties where T : ItemProperties
    {
        [SerializeField] private T itemProperties;
        private bool _isInstanceTypeInitialised;

        public override bool IsInstanceTypeInitialised => _isInstanceTypeInitialised;
        public override ItemProperties BaseItemProperties => itemProperties;
        public T GetItemProperties() => itemProperties;
        
        public void SetItemProperties(T item)
        {
            if (IsInstanceTypeInitialised) return;
            
            itemProperties = item;
            _isInstanceTypeInitialised = true;
        }

    }
}