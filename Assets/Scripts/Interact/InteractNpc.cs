using NPC;
using UnityEngine;

namespace Interact
{
    public class InteractNpc : MonoBehaviour, IInteract
    {
        private NpcController _npcController;

        public void Start()
        {
            _npcController = GetComponent<NpcController>();
        }

        public void Interact()
        {
            _npcController.TalkToPlayer();
        }
    }
}