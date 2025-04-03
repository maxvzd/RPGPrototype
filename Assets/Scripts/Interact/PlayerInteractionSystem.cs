using Constants;
using Interact.Contexts;
using Items;
using UI.Dialogue;
using UI.HUD;
using UnityEngine;

namespace Interact
{
    public class PlayerInteractionSystem : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float maxInteractDistance;
        private Inventory _inventorySystem;
        private HudManager _hud;

        private GameObject _currentlyAimedInteractable;
        private IInteract _interact;
        private DialogueManager _dialogueManager;

        private void Start()
        {
            _inventorySystem = GetComponent<Inventory>();
            _hud = GetComponent<HudManager>();
            _dialogueManager = GetComponent<DialogueManager>();
        }

        //Debug.DrawRay(origin, dir * maxInteractDistance, Color.red);
        private void Update()
        {
            var origin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
            var dir = mainCamera.transform.forward;
            var ray = new Ray(origin, dir);

            if (Physics.Raycast(ray, out var hit, maxInteractDistance))
            {
                var hitGameObject = hit.transform.gameObject;
                if (hitGameObject.CompareTag(TagConstants.InteractableTag))
                {
                    if (_currentlyAimedInteractable is not null && hitGameObject.GetInstanceID() == _currentlyAimedInteractable.GetInstanceID()) return;
                    _interact = hitGameObject.transform.root.GetComponent<IInteract>();
                    if (_interact is null) return;
                    
                    _currentlyAimedInteractable = hitGameObject;
                    _hud.UpdateInteractionIcon(_interact.IconName);
                    _hud.ShowInteractIcon();
                    return;
                }
            }

            if (!_hud.InteractionIconShowing) return;

            _hud.HideInteractIcon();
            _currentlyAimedInteractable = null;
        }

        public void Interact()
        {
            if (_currentlyAimedInteractable is null) return;
            var context = CreateInteractionContext(_interact);
            _interact.Interact(context);
        }
        
        private IInteractionContext CreateInteractionContext(IInteract interact)
        {
            return interact switch
            {
                IInteract<PickupContextBuilder> pickupInteraction => pickupInteraction.GetInteractionContext().AddInventoryContext(_inventorySystem),
                IInteract<SpeechContextBuilder> speechInteraction => speechInteraction.GetInteractionContext().Build(_dialogueManager),
                _ => new NoContext()
            };
        }
    }
}