using System;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Hunger")]
    public class HungerConsideration : UtilityConsideration
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var hunger = Entities.Npcs[id].NpcInfo.State.Hunger;
            return curve.Evaluate(hunger);
        }
    }
}