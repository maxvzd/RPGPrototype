using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPC.WorkerPrototyping
{
    public class WorkerBrain : MonoBehaviour
    {
        [SerializeField] private WorkerAction[] availableActions = Array.Empty<WorkerAction>();
        
        private WorkerAction _currentAction;
        private Guid Id { get; set; }

        public void Start()
        {
            var worker = GetComponent<Entity>();
            Id = worker.Id;
            
            StartCoroutine(CalculateNewDecision());
        }

        private IEnumerator CalculateNewDecision()
        {
            if (!availableActions.Any(x => x is not null)) yield break;
            while (true)
            {
                _currentAction = DecideBestAction();
                yield return _currentAction.Execute(Id);
            }
        }

        private WorkerAction DecideBestAction()
        {
            var scores = new Dictionary<WorkerAction, float>();
            foreach (var action in availableActions)
            {
                var score = action.CalculateScore(Id);
                scores.TryAdd(action, score);
            }
            return scores.OrderByDescending(x => x.Value).First().Key;
        }
    }
}