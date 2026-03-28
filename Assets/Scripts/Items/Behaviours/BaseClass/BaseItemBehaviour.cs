using System;
using Items.ItemInstances;
using Registries;
using UnityEngine;

namespace Items.Behaviours
{
    public abstract class BaseItemBehaviour : MonoBehaviour
    {
        public abstract BaseItemInstance Instance { get; }
        public abstract void SetInstance(BaseItemInstance instance);
        
        protected virtual void Awake()
        {
            ItemRegistry.Register(Instance);
        }
    }

    public abstract class BaseItemBehaviour<T> : BaseItemBehaviour where T : BaseItemInstance
    {
        [SerializeField] private T instance;
        public T TypedInstance => instance;
        public override BaseItemInstance Instance => TypedInstance;
        
        public override void SetInstance(BaseItemInstance instanceToSet)
        {
            instance = instanceToSet as T 
                       ?? throw new ArgumentException($"Instance of type {instance.GetType()} cannot be cast to type {typeof(T)}");
        }
    }
}