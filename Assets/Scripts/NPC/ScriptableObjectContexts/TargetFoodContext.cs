using System;
using NPC.Context;
using UnityEngine;

namespace NPC.ScriptableObjectContexts
{
    [CreateAssetMenu(menuName = "NPC/Context/TargetFood")]
    public class TargetFoodContext : ScriptableObjectContext
    {
        public override NpcContext Get(NpcState state)
        {
            var context = new NpcContext();
            var food = state.Food;
            context.Add(ContextKeys.Target, food.gameObject);
            
            return context;
        }
    }
}