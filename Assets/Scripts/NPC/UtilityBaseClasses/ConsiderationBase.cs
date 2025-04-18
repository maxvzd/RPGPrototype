﻿using System;
using NPC.Considerations;
using UnityEngine;

namespace NPC.UtilityBaseClasses
{
    //This class exists purely so I can assign stuff in the unity editor
    [Serializable]
    public abstract class ConsiderationBase : ScriptableObject
    {
        public abstract float Score(ConsiderationContextGenerator contextGenerator);
    }
}