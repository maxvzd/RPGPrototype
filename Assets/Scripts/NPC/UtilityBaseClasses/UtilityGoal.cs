using System;
using System.Collections;
using System.Linq;
using NPC.Context;
using UnityEngine;

namespace NPC.UtilityBaseClasses
{
    [CreateAssetMenu(menuName = "NPC/Goal")]
    public class UtilityGoal : ScriptableObject, IEvaluate
    {
        [SerializeField] private UtilityConsiderationBase[] considerations;
        [SerializeField] private UtilityAction[] actions;

        public static Guid GenerateReference() => Guid.NewGuid();
        
        public IEnumerator ExecuteActions(Guid id, NpcContext context)
        {
            var action =  UtilityAiUtilities.Evaluate(actions.ToDictionary(x => x,  x => context), id);
            yield return action.Execute(id, context);
        }

        public float Evaluate(Guid id, NpcContext context)
        {
            return UtilityAiUtilities.EvaluateConsiderations(id, considerations, context);
        }
    }
}