using System;
using NPC.UtilityBaseClasses;
using UnityEngine;
using NPC.Context;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/DistanceFromWork")]
    public class DistanceFromWork : UtilityConsideration
    {
        [SerializeField] protected AnimationCurve curve;
        
        public override float Score(Guid id, NpcContext context)
        {
            var work = EntitiesRegistry.NpcDictionary[id].NpcInfo.State.Work;
            var value = EntitiesRegistry.NpcDictionary[id].NpcInfo.State.IsAtDestination(work.position) ? 0 : 1f;
            return curve.Evaluate(value);
        }
    }
}