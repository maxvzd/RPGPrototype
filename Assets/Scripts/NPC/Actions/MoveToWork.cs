using System;
using System.Collections;
using NPC.Context;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/MoveToWork")]
    [Serializable]
    public class MoveToWork : UtilityAction
    {
        public override IEnumerator Execute(Guid id, NpcContext context)
        {
            var destination = Entities.Npcs[id].NpcInfo.State.Work.position;
            var controller = Entities.Npcs[id].NpcInfo.Controller;
            yield return controller.MoveToGameObject(destination);
        }
    }
}