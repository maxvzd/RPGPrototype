using System;
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

        public void Init(Guid id)
        {
            Id = id;
        }

        public void CalculateNewDecision()
        {
            if (!availableActions.Any(x => x is not null)) return;

            if (_currentAction is not null)
            {
                _currentAction.ActionFinished -= ActionFinished;
            }

            _currentAction = DecideBestAction();
            _currentAction.ActionFinished += ActionFinished;
            StartCoroutine(_currentAction.Execute(Id));
        }

        private void ActionFinished(object sender, EventArgs e)
        {
            CalculateNewDecision();
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