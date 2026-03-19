using System;
using System.Collections;
using NPC.Context;
using UnityEngine;

namespace NPC.UtilityBaseClasses
{
    public abstract class UtilityAction : ScriptableObject, IEvaluate
    {
        [SerializeField] private UtilityConsiderationBase[] considerations;
        
        public float Evaluate(Guid id, NpcContext context)
        {
            return UtilityAiUtilities.EvaluateConsiderations(id, considerations, context);    
        }

        public abstract IEnumerator Execute(Guid id, NpcContext context);
    }
}