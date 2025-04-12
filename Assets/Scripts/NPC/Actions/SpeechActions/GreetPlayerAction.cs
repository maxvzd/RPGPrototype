using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions.SpeechActions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Speech Actions/Greet player")]
    public class GreetPlayerAction : InstantAction
    {
        public override void Execute(NpcController npcController)
        {
            npcController.StopMoving();
            NpcController.StartDialog("Hello, it's so nice to meet you!!!!");
        }
    }
}