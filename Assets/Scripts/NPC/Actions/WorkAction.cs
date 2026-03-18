using System;
using System.Collections;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Work")]
    [Serializable]
    public class WorkAction : UtilityAction
    {
        public override IEnumerator Execute(Guid id)
        {
            var controller = Entities.Npcs[id].NpcInfo.Controller;
            yield return controller.Work();
        }
    }
}