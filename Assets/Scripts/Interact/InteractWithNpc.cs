using Interact;
using Interact.Contexts;
using NPC;
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
            if (context is SpeechContext speechContext)
            {
                Interact(speechContext);
            }
        }
        
        private void Interact(SpeechContext interactionContext)
        {
            _npc.DecideSpeechInteractionWithPlayer();
        }
    }
}

public class SpeechContextBuilder : IContextBuilder
{
    public SpeechContext Build()
    {
        return new SpeechContext();
    }
}