using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NPC.WorkerPrototyping
{
    public class WorkerController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        public EventHandler ActionFinished;
        private WorkerState _state;

        public void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _state = GetComponent<WorkerState>();
        }

        public void MoveToGameObject(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
            StartCoroutine(WaitUntilReachTarget());
        }
        
        private IEnumerator WaitUntilReachTarget()
        {		
            yield return new WaitUntil(() => _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance);
            ActionFinished.Invoke(this, EventArgs.Empty);
        }

        public void Work()
        {
            Debug.Log("Working");
            StartCoroutine(WorkCoroutine());
        }
        
        private IEnumerator WorkCoroutine()
        {		
            yield return new WaitForSeconds(5f);
            
            _state.AddHunger(5);
            _state.AddMoney(10);
            _state.RemoveEnergy(10);
            
            ActionFinished.Invoke(this, EventArgs.Empty);
        }

        public void Eat()
        {
            Debug.Log("Eating");
            StartCoroutine(EatCoroutine());
        }
        
        private IEnumerator EatCoroutine()
        {		
            yield return new WaitForSeconds(5f);
            
            _state.RemoveHunger(50);
            _state.RemoveMoney(StockMarket.FoodPrice);
            
            ActionFinished.Invoke(this, EventArgs.Empty);
        }

        public void Sleep()
        {
            Debug.Log("Sleeping zzzzz");
            StartCoroutine(SleepCoroutine());
        }
        
        private IEnumerator SleepCoroutine()
        {		
            yield return new WaitForSeconds(5f);
            
            _state.AddEnergy(50);
            _state.AddHunger(25);
            
            ActionFinished.Invoke(this, EventArgs.Empty);
        }
    }
}