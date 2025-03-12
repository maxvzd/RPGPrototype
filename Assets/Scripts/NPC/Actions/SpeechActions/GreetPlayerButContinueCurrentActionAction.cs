using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions.SpeechActions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Speech Actions/Greet player but continue current Action")]
    public class GreetPlayerButContinueCurrentActionAction : NpcAction
    {
        public override void Execute(NpcController npcController)
        {
            Debug.Log("What do you want?");
        }
    }
}