using System;
using NPC.Context;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Disposition")]
    public class DispositionConsideration : UtilityConsideration
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id, NpcContext context)
        {
            var disposition = EntitiesRegistry.NpcDictionary[id].NpcInfo.State.Disposition;
            return curve.Evaluate(disposition);
        }
    }
}