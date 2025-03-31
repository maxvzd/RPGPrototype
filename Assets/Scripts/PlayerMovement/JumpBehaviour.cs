using Constants;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace PlayerMovement
{
    public class JumpBehaviour : MonoBehaviour
    {
        [SerializeField] private float jumpHeight = 2f;
        private Grounded _grounded;
        private Animator _animator;
        private CharacterController _charController;
        private Vector3 _velocity;
        private const float GRAVITY = 9.81f;

        private bool _wasGroundedLastFrame;
        private bool _leftGround;

        private Vector3 _velocityAtJump;

        private bool IsGrounded => _grounded.IsGrounded;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _grounded = GetComponent<Grounded>();
            _charController = GetComponent<CharacterController>();
        }

        public void Jump()
        {
            if (!IsGrounded) return;

            _velocity.y = Mathf.Sqrt(jumpHeight * GRAVITY);
            _animator.SetTrigger(AnimatorConstants.JumpTrigger);
        }

        private void Update()
        {
            if (_wasGroundedLastFrame && !IsGrounded)
            {
                var animatorVelocity = _animator.velocity;
                _velocity.x = animatorVelocity.x * 1.5f;
                _velocity.z = animatorVelocity.z * 1.5f;
            }

            if (!_wasGroundedLastFrame && IsGrounded)
            {
                _velocity.x = 0f;
                _velocity.z = 0f;
            }
            
            _velocity.y -= GRAVITY * Time.deltaTime;
            _velocity.y = Mathf.Clamp(_velocity.y, 0, float.MaxValue);
            
            _charController.Move(_velocity * Time.deltaTime);
            _wasGroundedLastFrame = _grounded.IsGrounded;
        }
    }
}