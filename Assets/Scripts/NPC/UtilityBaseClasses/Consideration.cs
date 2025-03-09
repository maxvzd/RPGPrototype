using System;
using UnityEngine;

namespace NPC.UtilityBaseClasses
{
    [Serializable]
    public abstract class Consideration : ScriptableObject
    {
        public abstract float Score();
    }
}