using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Considerations
{
    [CreateAssetMenu(menuName = "Workers/Considerations/DistanceFromHome")]
    public class DistanceFromHome : WorkerConsideration
    {
        [SerializeField] protected AnimationCurve curve;
        
        
        public override float Score(Guid id)
        {
            var home = WorkerEntities.Workers[id].State.Home;
            var value = WorkerEntities.Workers[id].State.IsAtDestination(home.position) ? 0 : 1f;
            return curve.Evaluate(value);
        }
    }
}