using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions.SpeechActions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Speech Actions/Ignore")]
    public class IgnorePlayerAction : NpcAction
    {
        public override void Execute(NpcController npcController)
        {
            Debug.Log("I haven't got time for this");
        }
    }
}