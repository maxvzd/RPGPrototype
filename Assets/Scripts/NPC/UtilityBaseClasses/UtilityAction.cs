using System;
using System.Collections;
using UnityEngine;

namespace NPC.UtilityBaseClasses
{
    public abstract class UtilityAction : ScriptableObject, IEvaluate
    {
        [SerializeField] private WorkerConsiderationBase[] considerations;
        
        public float Evaluate(Guid id)
        {
            return UtilityAiUtilities.EvaluateConsiderations(id, considerations);    
        }

        public abstract IEnumerator Execute(Guid id);
    }
}