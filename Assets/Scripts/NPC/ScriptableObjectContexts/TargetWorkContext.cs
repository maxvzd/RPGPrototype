using System;
using NPC.Context;
using UnityEngine;

namespace NPC.ScriptableObjectContexts
{
    [CreateAssetMenu(menuName = "NPC/Context/TargetWork")]
    public class TargetWorkContext : ScriptableObjectContext
    {
        public override NpcContext Get(NpcState state)
        {
            var context = new NpcContext();
            var work = state.Work;
            context.Add(ContextKeys.Target, work.gameObject);
            
            return context;
        }
    }
}