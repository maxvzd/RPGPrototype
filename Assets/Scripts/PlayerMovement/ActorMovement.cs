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
        private bool _isMovementKeyDown;

        public bool ActorIsMoving => Mathf.Abs(_smoothedInput.x) > 0.1f || Mathf.Abs(_smoothedInput.y) > 0.1f;
        public Vector3 Direction => new(_smoothedInput.y, 0, _smoothedInput.x);
        public bool IsMovementKeysDown => _isMovementKeyDown;

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
            _isMovementKeyDown = movement.x != 0 || movement.y != 0;
            
            
            if (movement.y > 0)
            {
                movement *= _movementSpeed;
            }
            
            _smoothedInput = Vector2.SmoothDamp(_smoothedInput, movement, ref _smoothInputVelocity, playerAccelerationTime);
            
            _animator.SetFloat(AnimatorConstants.Vertical, _smoothedInput.y);
            _animator.SetFloat(AnimatorConstants.Horizontal, _smoothedInput.x);
        }
    }
}
