using System.Collections;
using System.Collections.Generic;

namespace Helpers
{
    public class ReadonlyHashSet<T> : IEnumerable<T>
    {
        private readonly HashSet<T> _set;

        public ReadonlyHashSet(HashSet<T> hashSet)
        {
            _set = hashSet;
        }
        
        public bool Contains(T item) => _set.Contains(item);
        public IEnumerator<T> GetEnumerator() => _set.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}