using System;
using NPC.Context;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Proximity")]
    public class ProximityConsideration : UtilityConsiderationBase
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id, NpcContext context)
        {
            if (!context.TryGet(ContextKeys.Target, out var target)) return 0;
            
            var remainingDistance = Entities.Npcs[id].NpcInfo.State.RemainingDistance(target.transform.position);
            return curve.Evaluate(remainingDistance);
        }
    }
}