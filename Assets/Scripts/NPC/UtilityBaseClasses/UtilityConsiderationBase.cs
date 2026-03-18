using System;
using UnityEngine;

namespace NPC.UtilityBaseClasses
{
    [Serializable]
    public abstract class WorkerConsiderationBase : ScriptableObject
    {
        public abstract float Score(Guid id);
    }
}