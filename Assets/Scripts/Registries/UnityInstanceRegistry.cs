using System.Collections.Generic;

namespace Registries
{
    public class UnityInstanceRegistry<T> : Registry<T>  where T : IEntity, IUnityEntity 
    {
        private static readonly Dictionary<int, T> Instances = new();
        public static IReadOnlyDictionary<int, T> ByInstanceId => Instances;
        
        public new static bool Register(T item)
        {
            return Guids.TryAdd(item.Id, item) && Instances.TryAdd(item.GetGameObjectInstanceID(), item);
        }
    }

    public interface IUnityEntity
    {
        int GetGameObjectInstanceID();
    }
}