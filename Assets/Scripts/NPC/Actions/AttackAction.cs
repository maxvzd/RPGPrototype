using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Attack")]
    public class AttackAction : NpcAction
    {
        public override void Execute(NpcController npcController)
        {
            //Debug.Log("Im attacking");
        }
    }
}