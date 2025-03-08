using Constants;
using UnityEngine;

namespace PlayerMovement
{ 
    public class ActorMovement : MonoBehaviour
    {
        [SerializeField] private float playerAccelerationTime;
        private float _movementSpeed = 1f;

        private Animator _animator;
        private Vector2 _smoothedInput;
        private Vector2 _smoothInputVelocity;

        public void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void ChangeSpeed(float amount)
        {
            _movementSpeed = Mathf.Clamp(_movementSpeed + amount, 0.1f, 2f);
        }

        public void Move(Vector2 movement)
        {
            movement *= _movementSpeed;
            
            _smoothedInput = Vector2.SmoothDamp(_smoothedInput, movement, ref _smoothInputVelocity, playerAccelerationTime);
            
            _animator.SetFloat(AnimatorConstants.Vertical, _smoothedInput.y);
            _animator.SetFloat(AnimatorConstants.Horizontal, _smoothedInput.x);
        }
    }
}
