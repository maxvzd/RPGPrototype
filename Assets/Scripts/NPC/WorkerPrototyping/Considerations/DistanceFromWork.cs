using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Considerations
{
    [CreateAssetMenu(menuName = "Workers/Considerations/DistanceFromWork")]
    public class DistanceFromWork : WorkerConsideration
    {
        [SerializeField] protected AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var work = WorkerEntities.Workers[id].State.Work;
            var value = WorkerEntities.Workers[id].State.IsAtDestination(work.position) ? 0 : 1f;
            return curve.Evaluate(value);
        }
    }
}