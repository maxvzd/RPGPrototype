using Constants;
using Interact;
using UnityEngine;

public class PlayerInteractionSystem : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxInteractDistance;
    
    public void Interact()
    {
        Vector3 origin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
        var sphereCastRadius = .1f;
        
        Vector3 dir = mainCamera.transform.forward;
        Ray sphereRay = new Ray(origin, dir);

        if (!Physics.SphereCast(sphereRay, sphereCastRadius, out RaycastHit hit, maxInteractDistance)) return;
        
        var hitGameObject = hit.transform.gameObject;
        if (hitGameObject.CompareTag(TagConstants.InteractableTag))
        {
            Debug.DrawRay(origin, dir * maxInteractDistance, Color.green, .1f);

            var interact = hitGameObject.GetComponent<IInteract>();
            interact?.Interact();
        }
        else
        {
            Debug.DrawRay(origin, dir * maxInteractDistance, Color.red, .1f);
        }
    }
}