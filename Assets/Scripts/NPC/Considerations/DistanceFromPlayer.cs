using System;
using NPC.UtilityBaseClasses;
using UnityEngine;
using NPC.Context;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/DistanceFromHome")]
    public class DistanceFromPlayer : UtilityConsideration
    {
        [SerializeField] protected AnimationCurve curve;
        
        public override float Score(Guid id, NpcContext context)
        {
            var player = EntitiesRegistry.Player.gameObject;
            var value = EntitiesRegistry.NpcDictionary[id].NpcInfo.State.IsAtDestination(player.transform.position) ? 0 : 1f;
            return curve.Evaluate(value);
        }
    }
}