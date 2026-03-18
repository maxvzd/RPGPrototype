using System;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Money")]
    public class MoneyConsideration : UtilityConsideration
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var energy = Entities.Npcs[id].NpcInfo.State.Money;
            return curve.Evaluate(energy);
        }
    }
}