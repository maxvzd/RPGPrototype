using System;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/DistanceFromFood")]
    public class DistanceFromFood : UtilityConsideration
    {
        [SerializeField] protected AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var food = Entities.Npcs[id].NpcInfo.State.Food;
            var value = Entities.Npcs[id].NpcInfo.State.IsAtDestination(food.position) ? 0 : 1f;
            return curve.Evaluate(value);
        }
    }
}