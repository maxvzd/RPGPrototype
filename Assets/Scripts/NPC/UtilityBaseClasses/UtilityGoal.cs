using System;
using System.Collections;
using NPC.Context;
using UnityEngine;

namespace NPC.UtilityBaseClasses
{
    [CreateAssetMenu(menuName = "Workers/Goal")]
    public class UtilityGoal : ScriptableObject, IEvaluate
    {
        [SerializeField] private WorkerConsiderationBase[] considerations;
        [SerializeField] private UtilityAction[] actions;

        public static Guid GenerateReference() => Guid.NewGuid();
        
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