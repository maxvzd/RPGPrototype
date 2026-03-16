using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NPC.WorkerPrototyping.Utilities;
using UnityEngine;

namespace NPC.WorkerPrototyping
{
    public class WorkerBrain
    {
        public EventHandler<IEnumerator> ExecuteCoroutine;
        
        private readonly List<WorkerGoal> _workerGoals;
        
        private Guid Id { get; }


        public WorkerBrain(Guid id, IEnumerable<WorkerGoal> workerGoals)
        {
            Id = id;
            _workerGoals = workerGoals.ToList();
        }

        private IEnumerator Run()
        {
            while (true)
            {
                if (!_workerGoals.Any(x => x is not null)) yield break;
                var goal = DecideBestGoal(_workerGoals);
                yield return goal.ExecuteActions(Id);
            }
        }

        public void Start()
        {
            ExecuteCoroutine?.Invoke(this, Run());
        }
        
        private WorkerGoal DecideBestGoal(IEnumerable<WorkerGoal> actions)
        {
            return UtilityAiUtilities.Evaluate(actions, Id);
        }

        public void SpottedEntity(Guid id)
        {
            //Load new goal
        }
    }
}