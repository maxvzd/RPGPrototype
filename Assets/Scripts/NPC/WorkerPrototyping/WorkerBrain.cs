using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPC.WorkerPrototyping
{
    public class WorkerBrain
    {
        private List<WorkerAction> _availableActions;
        private WorkerAction _currentAction;
        private Guid Id { get; }

        public WorkerBrain(Guid id, IEnumerable<WorkerAction> availableActions)
        {
            _availableActions = new List<WorkerAction>(availableActions);
            Id = id;
        }
        
        public void CalculateNewDecision()
        {
            if (!_availableActions.Any(x => x is not null)) return;

            if (_currentAction is not null)
            {
                _currentAction.ActionFinished -= ActionFinished;
            }

            _currentAction = DecideBestAction();
            _currentAction.ActionFinished += ActionFinished;
            _currentAction.Execute(Id);
        }

        private void ActionFinished(object sender, EventArgs e)
        {
            Debug.Log("Action finished!!1");
            CalculateNewDecision();
        }

        private WorkerAction DecideBestAction()
        {
            var scores = new Dictionary<WorkerAction, float>();
            foreach (var action in _availableActions)
            {
                var score = action.CalculateScore(Id);
                scores.TryAdd(action, score);
            }
            return scores.OrderByDescending(x => x.Value).First().Key;
        }
    }
}