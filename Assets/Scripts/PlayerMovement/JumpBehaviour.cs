using Constants;
using UnityEngine;

namespace PlayerMovement
{
    public class JumpBehaviour : MonoBehaviour
    {
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float verticalLength = 10f;
        private Grounded _grounded;
        private Animator _animator;

        private Rigidbody _rb;
        private ActorMovement _movement;
        private bool _isGrounded;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _grounded = GetComponent<Grounded>();
            
            _grounded.IsGrounded += (sender, args) =>
            {
                _animator.applyRootMotion = true;
                _isGrounded = true;
            };
            
            _grounded.IsNotGrounded += (sender, args) =>
            {
                _animator.applyRootMotion = false;
                _isGrounded = false;
            };
            
            _rb = GetComponent<Rigidbody>();
            _movement = GetComponent<ActorMovement>();
        }

        public void Jump()
        {
            if (!_isGrounded) return;

            _animator.SetTrigger(AnimatorConstants.JumpTrigger);
            
            var currentTransform = transform;
            var forward = currentTransform.forward * _movement.Direction.x * verticalLength;
            var right = currentTransform.right * _movement.Direction.z * verticalLength;
            var up = currentTransform.up;
            
            _rb.AddForce(up * jumpHeight, ForceMode.Impulse);
            _rb.linearVelocity = forward + right;
        }
    }
}