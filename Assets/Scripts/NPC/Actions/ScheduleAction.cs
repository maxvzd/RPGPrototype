using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Schedule Action")]
    public class ScheduleAction : NpcAction
    {
        public override void Execute(NpcController npcController)
        {
            npcController.FollowSchedule();
        }
    }
}