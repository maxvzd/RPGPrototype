using System;
using System.Collections;
using NPC.Context;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/MoveToTarget")]
    public class MoveToTargetAction : UtilityAction
    {
        public override IEnumerator Execute(Guid id, NpcContext context)
        {
            if (context.TryGet(ContextKeys.Target, out  var target))
            {
                yield return Entities.Npcs[id].NpcInfo.Controller.MoveToGameObject(target.transform.position);
            }
        }
    }
}