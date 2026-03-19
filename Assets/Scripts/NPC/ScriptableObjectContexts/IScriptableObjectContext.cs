using System;
using NPC.Context;

namespace NPC.ScriptableObjectContexts
{
    public interface IScriptableObjectContext
    {
        public NpcContext Get(NpcState state);
    }
}