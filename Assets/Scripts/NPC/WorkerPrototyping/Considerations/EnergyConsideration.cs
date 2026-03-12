using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Considerations
{
    [CreateAssetMenu(menuName = "Workers/Considerations/Energy")]
    public class EnergyConsideration : WorkerConsideration
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var energy = WorkerEntities.Workers[id].State.Energy;
            return curve.Evaluate(energy);
        }
    }
}