using System;
using UnityEngine;

namespace NPC.WorkerPrototyping
{
    [Serializable]
    public abstract class WorkerConsiderationBase : ScriptableObject
    {
        public abstract float Score(Guid id);
    }
}