using System.Collections.Generic;
using Interact.ContextBuilders;
using Interact.Contexts;
using NPC;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace Interact
{
    public class InteractWithNpc : MonoBehaviour, IInteract<SpeechInteractionContextBuilder>
    {
        public string IconName => "info-question";
        
        private NpcDecisionMaker _npc;
        
        public void Start()
        {
            _npc = GetComponent<NpcDecisionMaker>();
        }
        
        public SpeechInteractionContextBuilder GetInteractionContext()
        {
            return new SpeechInteractionContextBuilder();
        }

        public void Interact(IInteractionContext context)
        {
            if (context is SpeechInteractionContext speechContext)
            {
                Interact(speechContext);
            }
        }
        
        private void Interact(SpeechInteractionContext interactionContext)
        {
            var listOfActions = new List<NpcAction>
            {
                NpcActionsLoader.Instance.Actions["SpeechActionGreetStop"],
                NpcActionsLoader.Instance.Actions["SpeechActionGreetContinue"],
                NpcActionsLoader.Instance.Actions["SpeechActionIgnorePlayer"]
            };

            _npc.AddSpeechAction(listOfActions);
            _npc.CalculateNewDecision();
        }
    }
}