using System;
using Interact.Contexts;
using NPC;
using UnityEngine;

namespace Interact
{
    public class InteractWithNpc : MonoBehaviour, IInteract<SpeechContext>
    {
        private NpcDecisionMaker _npc;

        public void Start()
        {
            _npc = GetComponent<NpcDecisionMaker>();
        }

        private void Interact(SpeechContext interactionContext)
        {
            _npc.DecideSpeechInteractionWithPlayer();
        }
        
        public void Interact(IInteractionContext context)
        {
            Interact(context as SpeechContext);
        }
        
        public Type GetInteractionType() => typeof(SpeechContext);
    }
}