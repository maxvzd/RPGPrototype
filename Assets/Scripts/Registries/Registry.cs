using System;
using System.Collections.Generic;

namespace Registries
{
    public class Registry<T> where T : IEntity
    {
        protected static readonly Dictionary<Guid, T> Guids = new();
        public static IReadOnlyDictionary<Guid, T> ByGuid => Guids;
        
        public static bool Register(T item)
        {
            return Guids.TryAdd(item.Id, item);// && Instances.TryAdd(item.GetInstanceID(), item);
        }
    }
}