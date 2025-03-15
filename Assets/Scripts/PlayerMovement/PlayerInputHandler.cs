using Combat;
using Constants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovement
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private ActorMovement _movement;
        private CameraLook _cameraLook;
        private FirstPersonCameraSwap _armSwap;
        private SheatheManager _sheathe;
        private PlayerAttack _playerAttack;
        private PlayerInteractionSystem _interactionSystem;
        
        private PlayerInput _input;
        private InputAction _moveAction;
        private InputAction _increaseSpeedAction;
        private InputAction _lookAction;
        private InputAction _raiseWeaponAction;
        private InputAction _interactAction;
        private InputAction _attackAction;

        public void Start()
        {
            _movement = GetComponent<ActorMovement>();
            _cameraLook = GetComponent<CameraLook>();
            _armSwap = GetComponent<FirstPersonCameraSwap>();
            _sheathe = GetComponentInChildren<SheatheManager>();
            _interactionSystem = GetComponentInChildren<PlayerInteractionSystem>();
            _playerAttack = GetComponentInChildren<PlayerAttack>();

            _input = GetComponent<PlayerInput>();
            _moveAction = _input.actions[InputConstants.MoveAction];
            _increaseSpeedAction = _input.actions[InputConstants.ChangeSpeed];
            _lookAction = _input.actions[InputConstants.Look];
            _raiseWeaponAction = _input.actions[InputConstants.RaiseWeapon];
            _interactAction = _input.actions[InputConstants.Interact];
            _attackAction = _input.actions[InputConstants.Attack];
        }

        public void Update()
        {
            var movementInput = _moveAction.ReadValue<Vector2>();
            _movement.Move(movementInput);
            _playerAttack.SetMovementInput(movementInput);
            _movement.ChangeSpeed(_increaseSpeedAction.ReadValue<float>() * 0.1f);
            _cameraLook.MoveCamera(_lookAction.ReadValue<Vector2>());
            _cameraLook.TiltCamera(movementInput.x);
            
            if (_raiseWeaponAction.WasCompletedThisFrame())
            {
                _armSwap.SwitchArms();
                _sheathe.SheatheWeapon();
            }
            if (_interactAction.WasCompletedThisFrame())
            {
                _interactionSystem.Interact();
            }

            if (_attackAction.WasPressedThisFrame())
            {
                _playerAttack.HoldAttack();
            }

            if (_attackAction.WasReleasedThisFrame())
            {
                _playerAttack.ReleaseAttack();
            }
        }
    }
}
