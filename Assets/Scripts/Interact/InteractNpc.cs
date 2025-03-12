using NPC;
using UnityEngine;

namespace Interact
{
    public class InteractNpc : MonoBehaviour, IInteract
    {
        private NpcDecisionMaker _npc;

        public void Start()
        {
            _npc = GetComponent<NpcDecisionMaker>();
        }

        public void Interact()
        {
            _npc.DecideSpeechInteractionWithPlayer();
        }
    }
}