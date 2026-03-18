using System;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/DistanceFromHome")]
    public class DistanceFromHome : UtilityConsideration
    {
        [SerializeField] protected AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var home = Entities.Npcs[id].NpcInfo.State.Home;
            var value = Entities.Npcs[id].NpcInfo.State.IsAtDestination(home.position) ? 0 : 1f;
            return curve.Evaluate(value);
        }
    }
}