using System;
using NPC.UtilityBaseClasses;
using UnityEngine;
using NPC.Context;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/DistanceFromHome")]
    public class DistanceFromHome : UtilityConsideration
    {
        [SerializeField] protected AnimationCurve curve;
        
        public override float Score(Guid id, NpcContext context)
        {
            var home = EntitiesRegistry.NpcDictionary[id].NpcInfo.State.Home;
            var value = EntitiesRegistry.NpcDictionary[id].NpcInfo.State.IsAtDestination(home.position) ? 0 : 1f;
            return curve.Evaluate(value);
        }
    }
}