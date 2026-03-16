using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Considerations
{
    public class DispositionConsideration : WorkerConsideration
    {
        [SerializeField] private AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var disposition = WorkerEntities.Workers[id].State.Disposition;
            return curve.Evaluate(disposition);
        }
    }
}