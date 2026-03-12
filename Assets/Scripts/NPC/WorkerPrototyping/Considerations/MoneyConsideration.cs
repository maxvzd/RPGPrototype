using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Considerations
{
    [CreateAssetMenu(menuName = "Workers/Considerations/Money")]
    public class MoneyConsideration : WorkerConsideration
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var energy = WorkerEntities.Workers[id].State.Money;
            return curve.Evaluate(energy);
        }
    }
}