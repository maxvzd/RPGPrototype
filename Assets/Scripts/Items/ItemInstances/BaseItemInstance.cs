using System;
using Items.ItemDefinitions;
using UnityEngine;

namespace Items.ItemInstances
{
    [Serializable]
    public abstract class BaseItemInstance : IEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public abstract ItemDefinition BaseDefinition { get; }
    }
    
    [Serializable]
    public abstract class BaseItemInstance<T> : BaseItemInstance where T : ItemDefinition
    {
        [SerializeField] private T definition;
        public T ItemDefinition => definition;
        public override ItemDefinition BaseDefinition => definition;
        
        [SerializeField] private string myMessage;
        public string MyMessage => myMessage;

        public void SetMessage()
        {
            myMessage = "has been set";
        }
    }
}