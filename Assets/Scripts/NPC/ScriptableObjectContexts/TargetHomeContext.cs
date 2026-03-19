using System;
using NPC.Context;
using UnityEngine;

namespace NPC.ScriptableObjectContexts
{
    [CreateAssetMenu(menuName = "NPC/Context/TargetHome")]
    public class TargetHomeContext : ScriptableObjectContext
    {
        public override NpcContext Get(NpcState state)
        {
            var context = new NpcContext();
            var home = state.Home;
            context.Add(ContextKeys.Target, home.gameObject);
            
            return context;
        }
    }
}