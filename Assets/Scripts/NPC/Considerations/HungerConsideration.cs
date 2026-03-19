using System;
using NPC.UtilityBaseClasses;
using UnityEngine;
using NPC.Context;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Hunger")]
    public class HungerConsideration : UtilityConsideration
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id, NpcContext context)
        {
            var hunger = Entities.Npcs[id].NpcInfo.State.Hunger;
            return curve.Evaluate(hunger);
        }
    }
}