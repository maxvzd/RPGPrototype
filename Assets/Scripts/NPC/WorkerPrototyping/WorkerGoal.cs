using System;
using System.Collections;
using NPC.WorkerPrototyping.Utilities;
using UnityEngine;

namespace NPC.WorkerPrototyping
{
    [CreateAssetMenu(menuName = "Workers/Goal")]
    public class WorkerGoal : ScriptableObject, IEvaluate
    {
        [SerializeField] private WorkerConsiderationBase[] considerations;
        [SerializeField] private WorkerAction[] actions;

        public IEnumerator ExecuteActions(Guid id)
        {
            var action =  UtilityAiUtilities.Evaluate(actions, id);
            yield return action.Execute(id);
        }
        
        public float Evaluate(Guid id)
        {
            return UtilityAiUtilities.EvaluateConsiderations(id, considerations);
        }
    }
}