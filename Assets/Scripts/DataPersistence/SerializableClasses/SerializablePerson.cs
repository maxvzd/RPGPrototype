using System;
using System.Collections.Generic;
using System.Linq;
using DataPersistence.SerializableClasses.OnObject;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace DataPersistence.SerializableClasses
{
    [Serializable]
    public class SerializablePerson : ISerializable
    {
        [SerializeField]
        private List<NpcAction> actions;
        [SerializeField]
        private string name;
        [SerializeField]
        private Vector3 position;
        
        public Vector3 Position => position; 

        public SerializablePerson(string name, IEnumerable<NpcAction> actions, Vector3 position)
        {
            this.name = name;
            this.actions = actions.ToList();
            this.position = position;
        }
    }
}