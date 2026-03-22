using System.Collections.Generic;

namespace DataPersistence.SmartObjects
{
    public static class SmartObjectRegistry
    {
        private static readonly Dictionary<string, SmartObject> SmartObjects = new();
        public static IReadOnlyDictionary<string, SmartObject> Dictionary => SmartObjects;
        
        public static bool Register(SmartObject smartObject)
        {
            return SmartObjects.TryAdd(smartObject.Key, smartObject);
        }
    }
}