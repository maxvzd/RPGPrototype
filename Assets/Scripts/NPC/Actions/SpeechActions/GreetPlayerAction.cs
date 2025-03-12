using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions.SpeechActions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Speech Actions/Greet player")]
    public class GreetPlayerAction : NpcAction
    {
        public override void Execute(NpcController npcController)
        {
            npcController.Stop();
            Debug.Log("Hello, it's so nice to meet you!!!!");
        }
    }
}