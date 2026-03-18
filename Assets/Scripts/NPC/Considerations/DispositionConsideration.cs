using System;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Disposition")]
    public class DispositionConsideration : UtilityConsideration
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var disposition = Entities.Npcs[id].NpcInfo.State.Disposition;
            return curve.Evaluate(disposition);
        }
    }
}