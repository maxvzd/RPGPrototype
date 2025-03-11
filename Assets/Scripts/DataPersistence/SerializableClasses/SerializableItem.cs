using System;
using DataPersistence.SerializableClasses.OnObject;
using UnityEngine;

namespace DataPersistence.SerializableClasses
{
    [Serializable]
    public class SerializableItem : ISerializable
    {
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private float weight;

        public SerializableItem(string name, string description, float weight)
        {
            this.name = name;
            this.description = description;
            this.weight = weight;
        }
    }
}