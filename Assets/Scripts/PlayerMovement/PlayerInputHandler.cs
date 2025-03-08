using Constants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovement
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private ActorMovement _movement;
        private CameraLook _cameraLook;
        
        private PlayerInput _input;
        private InputAction _moveAction;
        private InputAction _increaseSpeedAction;
        private InputAction _lookAction;

        public void Start()
        {
            _movement = GetComponent<ActorMovement>();
            _cameraLook = GetComponent<CameraLook>();

            _input = GetComponent<PlayerInput>();
            _moveAction = _input.actions[InputConstants.MoveAction];
            _increaseSpeedAction = _input.actions[InputConstants.ChangeSpeed];
            _lookAction = _input.actions[InputConstants.Look];
        }

        public void Update()
        {
            _movement.Move(_moveAction.ReadValue<Vector2>());
            _movement.ChangeSpeed(_increaseSpeedAction.ReadValue<float>() * 0.1f);
            _cameraLook.MoveCamera(_lookAction.ReadValue<Vector2>());
        }
    }
}
