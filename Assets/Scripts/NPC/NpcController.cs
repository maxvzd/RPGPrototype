using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        public bool IsIdle { get; private set; } = true;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveToDestination(Vector3 destination)
        {
            IsIdle = false;
            _navMeshAgent.destination = destination;

            StartCoroutine(CheckRemainingDistanceCoroutine());
        }

        public IEnumerator CheckRemainingDistanceCoroutine()
        {
            while (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
            {
                yield return new WaitForEndOfFrame();
            }

            IsIdle = true;
        }
    }
}
