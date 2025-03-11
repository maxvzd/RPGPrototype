using DataPersistence.SerializableClasses;
using DataPersistence.SerializableClasses.OnObject;
using UnityEngine;

namespace NPC
{
    public class NpcDetails : MonoBehaviour, ISerializableGameObject<SerializablePerson>
    {
        [SerializeField] private string npcName;
    
        public SerializablePerson GetSerializable()
        {
            var decisionMaker = GetComponent<DecisionMaker>();

            return new SerializablePerson(npcName, decisionMaker.AvailableActions, transform.position);
        }
    }
}
