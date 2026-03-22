using System;
using System.Collections;
using NPC.Context;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Sleep")]
    [Serializable]
    public class SleepAction: UtilityAction
    {
        
        public override IEnumerator Execute(Guid id, NpcContext context)
        {
            var controller = EntitiesRegistry.NpcDictionary[id].NpcInfo.Controller;
            yield return controller.Sleep();
        }
    }
}