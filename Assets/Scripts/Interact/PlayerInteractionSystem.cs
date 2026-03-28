using Registries;
using UI.HUD;
using UnityEngine;

namespace Interact
{
    public class PlayerInteractionSystem : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float maxInteractDistance;
        private HudManager _hud;

        private GameObject _currentlyAimedInteractable;
        private Interactable _interact;

        private void Start()
        {
            _hud = GetComponent<HudManager>();
        }

        //Debug.DrawRay(origin, dir * maxInteractDistance, Color.red);
        private void Update()
        {
            var origin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
            var dir = mainCamera.transform.forward;
            var ray = new Ray(origin, dir);

            if (Physics.Raycast(ray, out var hit, maxInteractDistance, ~LayerMask.GetMask("Player")))
            {
                var hitGameObject = hit.transform.gameObject;
                if (_currentlyAimedInteractable is not null && hitGameObject.GetInstanceID() == _currentlyAimedInteractable.GetInstanceID()) return;
                if (InteractRegistry.ByInstanceId.TryGetValue(hitGameObject.GetInstanceID(), out var interact))
                {
                    _interact = interact;
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
            _interact.Interact();
        }
    }
}