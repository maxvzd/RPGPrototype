using UnityEngine;
using UnityEngine.AI;

namespace NPC.WorkerPrototyping
{
    public class WorkerState : MonoBehaviour
    {
        //[SerializeField] private WorkerState state;

        //public WorkerState State => state;
        
        [SerializeField] private float money;
        [SerializeField] private float energy;
        [SerializeField] private float hunger;
        [SerializeField] private Transform home;
        [SerializeField] private Transform food;
        [SerializeField] private Transform work;
        private NavMeshAgent _agent;

        public float Money => money; 
        public float Energy => energy; 
        public float Hunger => hunger;

        public Transform Home => home; 
        public Transform Food => food; 
        public Transform Work => work;
        

        public bool IsAtDestination(Vector3 destination)
        {
            var distance = Vector3.Distance(_agent.transform.position, destination);
            if (distance > _agent.stoppingDistance) return false;
            if (_agent.pathPending) return false;
            
            return _agent.remainingDistance <= _agent.stoppingDistance;
        }
        
        public void AddEnergy(float energyAmount)
        {
            energy += energyAmount;
        }
        
        public void AddMoney(float moneyAmount)
        {
            money += moneyAmount;
        }
        
        public void AddHunger(float hungerAmount)
        {
            hunger += hungerAmount;
        }
        
        public void RemoveEnergy(float energyAmount)
        {
            energy -= energyAmount;
        }
        
        public void RemoveMoney(float moneyAmount)
        {
            money -= moneyAmount;
        }
        
        public void RemoveHunger(float hungerAmount)
        {
            hunger -= hungerAmount;
        }

        public void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }
}