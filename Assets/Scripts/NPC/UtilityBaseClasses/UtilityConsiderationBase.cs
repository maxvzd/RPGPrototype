using System;
using NPC.Context;
using UnityEngine;

namespace NPC.UtilityBaseClasses
{
    [Serializable]
    public abstract class UtilityConsiderationBase : ScriptableObject
    {
        public abstract float Score(Guid id, NpcContext context);
    }
}