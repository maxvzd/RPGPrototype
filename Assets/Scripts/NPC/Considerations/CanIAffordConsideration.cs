using System;
using NPC.Context;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/CanIAffordFood")]
    public class CanIAffordConsideration : UtilityConsideration
    {
        public override float Score(Guid id, NpcContext context)
        {
            var money = EntitiesRegistry.NpcDictionary[id].NpcInfo.State.Money;

            return money > StockMarket.FoodPrice ? 1 : 0;
        }
    }
}