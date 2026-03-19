using System;
using NPC.Context;
using UnityEngine;

namespace NPC.ScriptableObjectContexts
{
    public abstract class ScriptableObjectContext : ScriptableObject, IScriptableObjectContext
    {
        public abstract NpcContext Get(NpcState state);
    }
}