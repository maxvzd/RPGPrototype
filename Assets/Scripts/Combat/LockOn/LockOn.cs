using System;
using System.Collections.Generic;
using NPC;
using UnityEngine;

namespace Combat.LockOn
{
    public class LockOn : MonoBehaviour
    {
        private LockOnTarget _target;
        private readonly Dictionary<Guid, LockOnTarget> _targets = new();
        
        [SerializeField] private Transform cameraContainer;
        [SerializeField] private SphereCollider lockOnCollider;
        [SerializeField] private float lockOnDistance;
        [SerializeField] private float cameraLockOnDegPerSec;
        [SerializeField] private float bodyLockOnDegPerSec;
        [SerializeField] private float fieldOfView;
        
        private float _cameraPitchVelocity;
        private float _cameraYawVelocity;
        private float _cameraRollVelocity;
        private float _bodyPitchVelocity;
        private float _bodyYawVelocity;
        private float _bodyRollVelocity;
        private LockOnTriggerHandler _lockOnEvents;
        private Guid? _cameraLockToken;

        public void Start()
        {
            _lockOnEvents = GetComponentInChildren<LockOnTriggerHandler>();
            _lockOnEvents.OnEnter += OnEnter;
            _lockOnEvents.OnExit += OnExit;
        }

        private void OnEnter(object sender, Collider e)
        {
            var target = e.gameObject.GetComponentInChildren<LockOnTarget>();
            if (!target || target.Id == Guid.Empty) return;
            _targets.Add(target.Id, target);
        }
        
        private void OnExit(object sender, Collider e)
        {
            var target = e.gameObject.GetComponentInChildren<LockOnTarget>();
            if (target) _targets.Remove(target.Id);
        }

        public void OnValidate()
        {
            if(lockOnCollider) 
                lockOnCollider.radius = lockOnDistance;
        }
        
        public void Update()
        {
            if (_target is null || !CanSeeTarget(_target))
            {
                BreakTargetLock();
                return;
            }

            _cameraLockToken ??= EntitiesRegistry.Player.CameraLook.RegisterCameraLock();
                
            var bodyHeading = _target.LookAtTransform.position - transform.position;
            var cameraHeading = _target.LookAtTransform.position - cameraContainer.transform.position;
                
            var targetBodyRotation = Quaternion.LookRotation(bodyHeading, transform.up);
            targetBodyRotation.z = 0;
            targetBodyRotation.x = 0;
                
            var targetCameraRotation = Quaternion.LookRotation(cameraHeading, transform.up);
                
            cameraContainer.rotation = DistanceBaseDampRotationTowardsTarget(targetCameraRotation, cameraContainer.rotation, cameraLockOnDegPerSec);
            transform.rotation = DampRotationTowardsTarget(targetBodyRotation, transform.rotation, bodyLockOnDegPerSec);
        }

        public void LockOnToNearestTarget()
        {
            if(_target is not null) return;
            
            var smallestAngle = float.MaxValue;
            foreach (var target in _targets.Values)
            {
                var angleToTarget = CalculateAngleToTarget(target);
                
                if (angleToTarget > fieldOfView / 2) continue;
                
                if (!(angleToTarget < smallestAngle)) continue;
                
                smallestAngle = angleToTarget;
                SetTarget(target);
            }
        }

        public void CycleTargetRight()
        {
            if (_target is null) return;
            
            var smallestAngle = float.MaxValue;
            var angleToCurrentTarget = CalculateAngleToTarget(_target);
            var currentTarget = _target;
            
            foreach (var target in _targets.Values)
            {
                if (target.Id == currentTarget.Id) continue;
                
                var angleToTarget = CalculateAngleToTarget(target);
                
                if (angleToTarget > smallestAngle || angleToTarget < angleToCurrentTarget) continue;
                    
                smallestAngle = angleToTarget;
                SetTarget(target);
            }
        }
        
        public void CycleTargetLeft()
        {
            if (_target is null) return;
            
            var largestAngle = float.MinValue;
            var angleToCurrentTarget = CalculateAngleToTarget(_target);
            var currentTarget = _target;
            
            foreach (var target in _targets.Values)
            {
                if (target.Id == currentTarget.Id) continue;
                
                var angleToTarget = CalculateAngleToTarget(target);
                if (angleToTarget < largestAngle || angleToTarget > angleToCurrentTarget) continue;
                    
                largestAngle = angleToTarget;
                SetTarget(target);
            }
        }

        private bool CanSeeTarget(LockOnTarget target)
        {
            var directionToTarget = (target.LookAtTransform.position - cameraContainer.position).normalized;
            if (!Physics.Raycast(
                    cameraContainer.position, 
                    directionToTarget, out var hit, 
                    lockOnDistance, 
                    ~LayerMask.GetMask("Player", "VisibleInFirstPerson"))) 
                return false;
            
            return hit.transform.gameObject == target.gameObject;
        }

        private void SetTarget(LockOnTarget target)
        {
            if(CanSeeTarget(target))
                _target = target;
        }
        
        public void BreakTargetLock()
        {
            _target = null;
            if (_cameraLockToken is not null)
            {
                EntitiesRegistry.Player.CameraLook.UnRegisterCameraLock(_cameraLockToken.Value);
                _cameraLockToken = null;
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            foreach (var target in _targets.Values)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(target.LookAtTransform.position, .5f);
            }
        }
        
        private float CalculateAngleToTarget(LockOnTarget target)
        {
            var directionToCurrentTarget =  (target.transform.position - transform.position).normalized;
            return Vector3.SignedAngle(transform.forward, directionToCurrentTarget, Vector3.up);
        }
        
        private static Quaternion DampRotationTowardsTarget(Quaternion target, Quaternion current, float speed)
        {
            var maxDegreesDelta = speed * Time.deltaTime;
            return Quaternion.RotateTowards(current, target, maxDegreesDelta);
        }
        
        private static Quaternion DistanceBaseDampRotationTowardsTarget(Quaternion target, Quaternion current, float speed)
        {
            var angle = Quaternion.Angle(current, target);
            var speedFactor = Mathf.Max(angle / 180f, 0.5f);
            
            var maxDegreesDelta = speed * speedFactor * Time.deltaTime;
            return Quaternion.RotateTowards(current, target, maxDegreesDelta);
        }
    }
}