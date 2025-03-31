using System;
using System.Linq;
using Constants;
using UnityEngine;
using UnityEngine.AI;

namespace PlayerMovement
{
    public class Grounded : MonoBehaviour
    {
        [SerializeField] private float maxCastDepth;
        
        private Vector3 _halfExtents;
        private Animator _animator;
        
        public EventHandler IsGrounded;
        public EventHandler IsNotGrounded;

        private bool _isGrounded;
        private bool _wasGroundedLastFrame;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            var radius = GetComponent<CapsuleCollider>().radius;
            
            _halfExtents = new Vector3(radius, maxCastDepth, radius);
        }

        private void Update()
        {
            DetectIsGrounded();
            Debug.DrawRay(transform.position, -transform.up * maxCastDepth, Color.cyan);
        }

        private void DetectIsGrounded()
        {
            var currentTransform = transform;
            _isGrounded = Physics.BoxCastAll(currentTransform.position, _halfExtents, -currentTransform.up, currentTransform.rotation, maxCastDepth, LayerMask.GetMask(LayerConstants.Terrain)).Any();
            _animator.SetBool(AnimatorConstants.IsGrounded, _isGrounded);

            switch (_isGrounded)
            {
                case true when !_wasGroundedLastFrame:
                    IsGrounded?.Invoke(this, EventArgs.Empty);
                    break;
                case false when _wasGroundedLastFrame:
                    IsNotGrounded?.Invoke(this, EventArgs.Empty);
                    break;
            }
            
            _wasGroundedLastFrame = _isGrounded;
        }
    }
}
