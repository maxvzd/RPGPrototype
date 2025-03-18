using System;
using Constants;
using Interact.Contexts;
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

        private void Start()
        {
            _inventorySystem = GetComponent<Inventory>();
            _hud = GetComponent<HudManager>();
        }

        private void Update()
        {
            var origin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
            //const float sphereCastRadius = .1f;
        
            var dir = mainCamera.transform.forward;
            var ray = new Ray(origin, dir);

            //if (!Physics.SphereCast(sphereRay, sphereCastRadius, out RaycastHit hit, maxInteractDistance)) return;
            if (Physics.Raycast(ray, out RaycastHit hit, maxInteractDistance))
            {
                var hitGameObject = hit.transform.gameObject;
                if (hitGameObject.CompareTag(TagConstants.InteractableTag))
                {
                    _hud.ShowInteractIcon();
                    _currentlyAimedInteractable = hitGameObject;
                    return;
                }
            }
            _hud.HideInteractIcon();
            _currentlyAimedInteractable = null;
        }

        public void Interact()
        {
            // var origin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
            // const float sphereCastRadius = .1f;
            //
            // var dir = mainCamera.transform.forward;
            // var sphereRay = new Ray(origin, dir);
            //
            // if (!Physics.SphereCast(sphereRay, sphereCastRadius, out RaycastHit hit, maxInteractDistance)) return;
            //
            // var hitGameObject = hit.transform.gameObject;
            // if (hitGameObject.CompareTag(TagConstants.InteractableTag))
            // {
            //     Debug.DrawRay(origin, dir * maxInteractDistance, Color.green, .1f);
            //
            //     var interact = hitGameObject.GetComponent<IInteract>();
            //     var context = CreateInteractionContext(interact.GetInteractionType());
            //     interact.Interact(context);
            // }
            // else
            // {
            //     Debug.DrawRay(origin, dir * maxInteractDistance, Color.red, .1f);
            // }
            if (_currentlyAimedInteractable is null) return;
            
            var interact = _currentlyAimedInteractable.GetComponent<IInteract>();
            var context = CreateInteractionContext(interact.GetInteractionType());
            interact.Interact(context);
        }

        private IInteractionContext CreateInteractionContext(Type interactionType)
        {
            if (typeof(SpeechContext).IsAssignableFrom(interactionType))
            {
                return new SpeechContext();
            }

            if (typeof(PickupContext).IsAssignableFrom(interactionType))
            {
                return new PickupContext(_inventorySystem);
            }

            return new NoContext();
        }
    }
}