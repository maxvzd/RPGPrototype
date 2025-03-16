using System;
using Constants;
using Interact;
using Interact.Contexts;
using UnityEngine;

public class PlayerInteractionSystem : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxInteractDistance;
    private Inventory _inventorySystem;

    private void Start()
    {
        _inventorySystem = GetComponent<Inventory>();
    }

    public void Interact()
    {
        var origin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
        const float sphereCastRadius = .1f;
        
        var dir = mainCamera.transform.forward;
        var sphereRay = new Ray(origin, dir);

        if (!Physics.SphereCast(sphereRay, sphereCastRadius, out RaycastHit hit, maxInteractDistance)) return;
        
        var hitGameObject = hit.transform.gameObject;
        if (hitGameObject.CompareTag(TagConstants.InteractableTag))
        {
            Debug.DrawRay(origin, dir * maxInteractDistance, Color.green, .1f);

            var interact = hitGameObject.GetComponent<IInteract>();
            var context = CreateInteractionContext(interact.GetInteractionType());
            interact.Interact(context);
        }
        else
        {
            Debug.DrawRay(origin, dir * maxInteractDistance, Color.red, .1f);
        }
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