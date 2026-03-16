using System;
using System.Collections;
using NPC.WorkerPrototyping.Utilities;
using UnityEngine;

namespace NPC.WorkerPrototyping
{
    public abstract class WorkerAction : ScriptableObject, IEvaluate
    {
        [SerializeField] private WorkerConsiderationBase[] considerations;
        
        public float Evaluate(Guid id)
        {
            return UtilityAiUtilities.EvaluateConsiderations(id, considerations);    
        }

        public abstract IEnumerator Execute(Guid id);
    }
}