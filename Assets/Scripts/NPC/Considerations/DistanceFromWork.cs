using System;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "Workers/Considerations/DistanceFromWork")]
    public class DistanceFromWork : UtilityConsideration
    {
        [SerializeField] protected AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var work = Entities.Npcs[id].NpcInfo.State.Work;
            var value = Entities.Npcs[id].NpcInfo.State.IsAtDestination(work.position) ? 0 : 1f;
            return curve.Evaluate(value);
        }
    }
}