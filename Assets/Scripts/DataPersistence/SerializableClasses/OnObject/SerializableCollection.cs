using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataPersistence.SerializableClasses.OnObject
{
    [Serializable]
    public class SerializableCollection
    {
        [SerializeField]
        private IEnumerable<ISerializable> collection;

        public SerializableCollection(IEnumerable<ISerializable> serializableItems)
        {
            collection = serializableItems.ToList();
        }
    }
}