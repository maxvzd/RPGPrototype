using Items.Properties;

namespace Items.InstancePropertiesClasses
{
    public abstract class InstanceProperties
    {
        public abstract bool IsInstanceTypeInitialised { get; }
        public abstract ItemProperties Item { get; }
    }
    
    public abstract class InstanceProperties<T> : InstanceProperties where T : ItemProperties
    {
        private bool _isInstanceTypeInitialised;
        
        protected T ConcreteItem;

        public override bool IsInstanceTypeInitialised => _isInstanceTypeInitialised;
        public override ItemProperties Item => ConcreteItem;
        public T GetItemProperties() => ConcreteItem;
        
        public void SetItemProperties(T item)
        {
            if (IsInstanceTypeInitialised) return;
            
            ConcreteItem = item;
            _isInstanceTypeInitialised = true;
        }

    }
}