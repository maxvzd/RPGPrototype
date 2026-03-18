using System;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/CanIAffordFood")]
    public class CanIAffordConsideration : UtilityConsideration
    {
        public override float Score(Guid id)
        {
            var money = Entities.Npcs[id].NpcInfo.State.Money;

            return money > StockMarket.FoodPrice ? 1 : 0;
        }
    }
}