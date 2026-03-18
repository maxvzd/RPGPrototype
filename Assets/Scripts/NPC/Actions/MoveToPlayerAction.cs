using System;
using System.Collections;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/MoveToHome")]
    public class MoveToPlayerAction : UtilityAction
    {
        public override IEnumerator Execute(Guid id)
        {
            var player = Entities.Player.gameObject;
            yield return Entities.Npcs[id].NpcInfo.Controller.MoveToGameObject(player.transform.position);
        }
    }
}