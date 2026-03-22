using System;
using NPC.UtilityBaseClasses;
using UnityEngine;
using NPC.Context;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Energy")]
    public class EnergyConsideration : UtilityConsideration
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id, NpcContext context)
        {
            var energy = EntitiesRegistry.NpcDictionary[id].NpcInfo.State.Energy;
            return curve.Evaluate(energy);
        }
    }
}