using DataPersistence.SmartObjects;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcState : MonoBehaviour
    {
        [SerializeField] private float money;
        [SerializeField] private float energy;
        [SerializeField] private float hunger;
        [SerializeField] private string home;
        [SerializeField] private string food;
        [SerializeField] private string work;
        private NavMeshAgent _agent;
        private SocialStats _socialStats;
        private SmartObject _home;
        private SmartObject _food;
        private SmartObject _work;

        public float Money => money; 
        public float Energy => energy; 
        public float Hunger => hunger;
        public Transform Home => _home.GameObject.transform; 
        public Transform Food =>  _food.GameObject.transform; 
        public Transform Work =>  _work.GameObject.transform;
        public float Disposition => _socialStats.Disposition;
        
        
        public void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _socialStats = GetComponent<SocialStats>();
        }

        public void Start()
        {
            _home = SmartObjectRegistry.Dictionary[home];
            _food = SmartObjectRegistry.Dictionary[food];
            _work = SmartObjectRegistry.Dictionary[work];
        }

        public bool IsAtDestination(Vector3 destination)
        {
            var distance = Vector3.Distance(_agent.transform.position, destination);
            if (distance > _agent.stoppingDistance) return false;
            if (_agent.pathPending) return false;
            
            return _agent.remainingDistance <= _agent.stoppingDistance;
        }

        public float RemainingDistance(Vector3 target)
        {
            if(_agent.hasPath) return _agent.remainingDistance - _agent.stoppingDistance;
            return 0;
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
    }
}