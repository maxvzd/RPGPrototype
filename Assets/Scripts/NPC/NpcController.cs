using System;
using System.Collections;
using NPC.Scheduling;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Coroutine _currentCoroutine;
        private SocialStats _socialStats;
        private NpcSchedule _schedule;

        public EventHandler ReachedDestination;
        public float Disposition => _socialStats.Disposition;

        public void MoveToDestination(Vector3 destination)
        {
            if (_navMeshAgent.destination == destination) return;
            
            _navMeshAgent.destination = destination;
            StartDistanceCoroutine();
        }
        
        public void FollowSchedule()
        {
            _schedule.FollowSchedule();
        }
        
        public void StopFollowingSchedule()
        {
            _schedule.StopFollowingSchedule();
        }

        public void StopMoving()
        {
            _navMeshAgent.isStopped = !_navMeshAgent.isStopped;
        }

        private void StopDistanceCoroutine()
        {
            if (_currentCoroutine is not null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }

        private void StartDistanceCoroutine()
        {
            StopDistanceCoroutine();
            _currentCoroutine = StartCoroutine(CheckRemainingDistanceCoroutine());
        }

        private IEnumerator CheckRemainingDistanceCoroutine()
        {
            while (_navMeshAgent.remainingDistance >= _navMeshAgent.stoppingDistance)
            {
                yield return new WaitForEndOfFrame();
            }
            ReachedDestination?.Invoke(this, EventArgs.Empty);
        }
        
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _socialStats = GetComponent<SocialStats>();
            _schedule = GetComponent<NpcSchedule>();
        }
    }
}
