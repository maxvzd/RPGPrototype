using System;
using System.Collections;
using NPC.Context;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Work")]
    [Serializable]
    public class WorkAction : UtilityAction
    {
        public override IEnumerator Execute(Guid id, NpcContext context)
        {
            var controller = EntitiesRegistry.NpcDictionary[id].NpcInfo.Controller;
            yield return controller.Work();
        }
    }
}