using NPC;
using UnityEngine;

namespace DataPersistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        private IDataLoader _dataLoader;
        [SerializeField] private GameObject npcBase;
        
        public void Start()
        {
            _dataLoader = new FileLoader();

            var people = _dataLoader.LoadPeople();
            foreach (var person in people)
            {
                var npc = Instantiate(npcBase, person.Position, Quaternion.identity);
                var decisionMaker = npc.GetComponent<DecisionMaker>();
                if (decisionMaker is not null)
                {
                }
            }
        }
        
        
    }
}