using System;
using System.Collections;
using System.Collections.Generic;
using UI.Dialogue;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Coroutine _currentCoroutine;

        public EventHandler ReachedDestination;

        public void MoveToDestination(Vector3 destination)
        {
            if (_navMeshAgent.destination == destination) return;
            
            _navMeshAgent.destination = destination;
            StartDistanceCoroutine();
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
            while (_navMeshAgent.pathPending)
            {
                yield return null;
            }
            
            while (_navMeshAgent.remainingDistance >= _navMeshAgent.stoppingDistance)
            {
                yield return null;
            }
            
            Debug.Log("I've reached position");
            ReachedDestination?.Invoke(this, EventArgs.Empty);
        }
        
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public static void StartDialog(string dialogue)
        {
            var options = new List<string>();
            for (var i = 0; i < 5; i++)
            {
                options.Add($"This is option: {i}");
            }
            
            DialogueManager.Instance.PopulateDialogueOptions(dialogue, options);
            DialogueManager.Instance.ShowUI();
        }
    }
}
