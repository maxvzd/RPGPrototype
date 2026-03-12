using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Considerations
{
    [CreateAssetMenu(menuName = "Workers/Considerations/CanIAffordFood")]
    public class CanIAffordConsideration : WorkerConsideration
    {
        public override float Score(Guid id)
        {
            var money = WorkerEntities.Workers[id].State.Money;

            return money > StockMarket.FoodPrice ? 1 : 0;
        }
    }
}