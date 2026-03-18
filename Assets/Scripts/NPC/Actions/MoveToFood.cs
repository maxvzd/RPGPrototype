using System;
using System.Collections;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/MoveToFood")]
    [Serializable]
    public class MoveToFood : UtilityAction
    {
        public override IEnumerator Execute(Guid id)
        {
            var destination = Entities.Npcs[id].NpcInfo.State.Food.position;
            var controller = Entities.Npcs[id].NpcInfo.Controller;
            yield return controller.MoveToGameObject(destination);
            
        }
    }
}