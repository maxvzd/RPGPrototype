using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private IEnumerator _currentCoroutine;
        private SocialStats _socialStats;
        
        public bool IsIdle { get; private set; } = true;
        public float Disposition => _socialStats.Disposition;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _socialStats = GetComponent<SocialStats>();
        }

        public void MoveToDestination(Vector3 destination)
        {
            IsIdle = false;
            _navMeshAgent.destination = destination;
            StartStopCoroutine(CheckRemainingDistanceCoroutine());
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = !_navMeshAgent.isStopped;
        }

        private void StartStopCoroutine(IEnumerator coroutine)
        {
            if (_currentCoroutine is not null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine = coroutine;
            StartCoroutine(_currentCoroutine);
        }

        private IEnumerator CheckRemainingDistanceCoroutine()
        {
            while (_navMeshAgent.remainingDistance >= _navMeshAgent.stoppingDistance)
            {
                yield return new WaitForEndOfFrame();
            }
            IsIdle = true;
        }
    }
}
