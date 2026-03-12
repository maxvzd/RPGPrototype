using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Considerations
{
    [CreateAssetMenu(menuName = "Workers/Considerations/DistanceFromFood")]
    public class DistanceFromFood : WorkerConsideration
    {
        [SerializeField] protected AnimationCurve curve;
        
        public override float Score(Guid id)
        {
            var food = WorkerEntities.Workers[id].State.Food;
            var value = WorkerEntities.Workers[id].State.IsAtDestination(food.position) ? 0 : 1f;
            return curve.Evaluate(value);
        }
    }
}