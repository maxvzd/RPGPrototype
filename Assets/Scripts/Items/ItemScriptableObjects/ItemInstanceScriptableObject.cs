using Items.ItemInstances;
using UnityEngine;

namespace Items.ItemScriptableObjects
{
    public abstract class ItemInstanceScriptableObject : ScriptableObject
    {
        public abstract BaseItemInstance BaseInstance { get; }
    }
    
    public abstract class ItemInstanceScriptableObject<T> : ItemInstanceScriptableObject 
        where T : BaseItemInstance
    {
        [SerializeField] private T instance;
        public T Instance => instance;
        public override BaseItemInstance BaseInstance => instance;
    }
}