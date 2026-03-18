using System.Collections.Generic;
using UnityEngine;

namespace NPC.Context
{
    public class NpcContext
    {
        private readonly Dictionary<object, object> _context = new();

        public void Add<T>(ContextKey<T> key, T value)
        {
            _context[key] = value;
        }

        public T Get<T>(ContextKey<T> key)
        {
            return (T)_context[key];
        }
        
        public bool TryGet<T>(ContextKey<T> key, out T value)
        {
            if (_context.TryGetValue(key, out var rawValue))
            {
                value = (T)rawValue;
                return true;
            }
            value = default;
            return false;
        }
    }

    public class ContextKey<T> { }
    
    public static class ContextKeys
    {
        public static readonly ContextKey<GameObject> Target = new();
        public static readonly ContextKey<Vector3> TargetPosition = new();
    }
}