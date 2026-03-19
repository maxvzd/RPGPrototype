using System;
using NPC.UtilityBaseClasses;
using UnityEngine;
using NPC.Context;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/DistanceFromFood")]
    public class DistanceFromFood : UtilityConsideration
    {
        [SerializeField] protected AnimationCurve curve;
        
        public override float Score(Guid id, NpcContext context)
        {
            var food = Entities.Npcs[id].NpcInfo.State.Food;
            var value = Entities.Npcs[id].NpcInfo.State.IsAtDestination(food.position) ? 0 : 1f;
            return curve.Evaluate(value);
        }
    }
}