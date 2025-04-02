using Interact;
using Interact.Contexts;
using NPC;
using NPC.UtilityBaseClasses.Contexts;
using UnityEngine;

namespace Interact
{
    public class InteractWithNpc : MonoBehaviour, IInteract<SpeechContextBuilder>
    {
        public string IconName => "info-question";
        
        private NpcDecisionMaker _npc;
        
        public void Start()
        {
            _npc = GetComponent<NpcDecisionMaker>();
        }
        
        public SpeechContextBuilder GetInteractionContext()
        {
            return new SpeechContextBuilder();
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
            //_npc.CalculateNewDecision<SpeechConsiderationContext>();
        }
    }
}

public class SpeechContextBuilder : IContextBuilder
{
    public SpeechInteractionContext Build()
    {
        return new SpeechInteractionContext();
    }
}