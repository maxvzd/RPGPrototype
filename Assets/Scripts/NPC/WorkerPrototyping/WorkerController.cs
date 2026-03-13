using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NPC.WorkerPrototyping
{
    public class WorkerController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private WorkerState _state;

        public void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _state = GetComponent<WorkerState>();
        }

        public IEnumerator MoveToGameObject(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
            yield return new WaitUntil(() => _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance);
        }

        public IEnumerator Work()
        {
            yield return new WaitForSeconds(5f);
            
            _state.AddHunger(5);
            _state.AddMoney(10);
            _state.RemoveEnergy(10);
        }

        public IEnumerator Eat()
        {
            yield return new WaitForSeconds(5f);
            
            _state.RemoveHunger(50);
            _state.RemoveMoney(StockMarket.FoodPrice);
        }

        public IEnumerator Sleep()
        {
            yield return new WaitForSeconds(5f);
            
            _state.AddEnergy(50);
            _state.AddHunger(25);
        }
    }
}