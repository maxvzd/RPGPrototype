using DataPersistence.Database.Models;
using DataPersistence.SmartObjects;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcState
    {
        private string _name;
        private readonly NavMeshAgent _agent;
        private readonly SocialStats _socialStats;
        private readonly SmartObject _home;
        private readonly SmartObject _food;
        private readonly SmartObject _work;

        public float Money { get; private set; }
        public float Energy { get; private set;}
        public float Hunger { get;  private set;}
        public Transform Home => _home.GameObject.transform; 
        public Transform Food =>  _food.GameObject.transform; 
        public Transform Work =>  _work.GameObject.transform;
        public float Disposition => _socialStats.Disposition;
        
        public NpcState(DatabaseNpc npcState, NavMeshAgent agent, SocialStats socialStats)
        {
            _agent = agent;
            _socialStats = socialStats;
            
            Money = npcState.Money;
            Energy = npcState.Energy;
            Hunger = npcState.Hunger;
            _name = npcState.Name;
            
            var homeKey = npcState.HomeKey;
            _home =  SmartObjectRegistry.Dictionary[homeKey];
            
            var foodKey = npcState.FoodKey;
            _food =  SmartObjectRegistry.Dictionary[foodKey];
            
            var workKey = npcState.WorkKey;
            _work =  SmartObjectRegistry.Dictionary[workKey];
            
            _home = SmartObjectRegistry.Dictionary[homeKey];
            _food = SmartObjectRegistry.Dictionary[foodKey];
            _work = SmartObjectRegistry.Dictionary[workKey];
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
            Energy += energyAmount;
        }
        
        public void AddMoney(float moneyAmount)
        {
            Money += moneyAmount;
        }
        
        public void AddHunger(float hungerAmount)
        {
            Hunger += hungerAmount;
        }
        
        public void RemoveEnergy(float energyAmount)
        {
            Energy -= energyAmount;
        }
        
        public void RemoveMoney(float moneyAmount)
        {
            Money -= moneyAmount;
        }
        
        public void RemoveHunger(float hungerAmount)
        {
            Hunger -= hungerAmount;
        }
    }
}