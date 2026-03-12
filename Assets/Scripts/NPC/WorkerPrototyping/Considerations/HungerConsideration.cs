using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Considerations
{
    [CreateAssetMenu(menuName = "Workers/Considerations/Hunger")]
    public class HungerConsideration : WorkerConsideration
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var hunger = WorkerEntities.Workers[id].State.Hunger;
            return curve.Evaluate(hunger);
        }
    }
}