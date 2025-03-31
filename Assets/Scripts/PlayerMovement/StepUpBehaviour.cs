using System.Linq;
using Constants;
using PlayerMovement;
using UnityEngine;

public class StepUpBehaviour : MonoBehaviour
{
    [SerializeField] private float stepHeight;
    [SerializeField] private float detectionDepth;
    [SerializeField] private float heightOffset;
    [SerializeField] private float stepHeightTolerance;
    [SerializeField] private float upForce;
    private float _playerRadius;
    private ActorMovement _movement;

    private void Start()
    {
        _playerRadius = GetComponent<CapsuleCollider>().radius;
        _movement = GetComponent<ActorMovement>();
    }

    private void Update()
    {
        if (!_movement.IsMovementKeysDown) return;
        
        var currentTransform = transform;
        var forward = currentTransform.forward;
        var up = currentTransform.up;
        var origin = currentTransform.position + ((stepHeight / 2) + heightOffset) * up;

        var halfExtent = up * stepHeight / 2f;
        //var lowerOrigin = origin - halfExtent;
        var upperOrigin = origin + halfExtent;
        
        // Debug.DrawRay(origin, forward * detectionDepth, Color.red);
        // Debug.DrawRay(lowerOrigin, forward * detectionDepth, Color.cyan);
        // Debug.DrawRay(upperOrigin, forward * detectionDepth, Color.red);

        if (!Physics.BoxCastAll(origin, new Vector3(_playerRadius, stepHeight / 2f, detectionDepth), forward, Quaternion.identity, detectionDepth, LayerMask.GetMask(LayerConstants.Terrain)).Any()) return;
        
        var stepUpOrigin = upperOrigin + up * stepHeightTolerance;
        //Debug.DrawRay(stepUpOrigin, forward * detectionDepth, Color.green);
        
        if (Physics.BoxCastAll(stepUpOrigin, new Vector3(_playerRadius, 0.01f, detectionDepth), forward, Quaternion.identity, detectionDepth, LayerMask.GetMask(LayerConstants.Terrain)).Any()) return;
        transform.position += up * (upForce * Time.deltaTime);
    }
}
