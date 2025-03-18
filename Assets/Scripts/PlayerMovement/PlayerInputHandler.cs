using Combat;
using Constants;
using Interact;
using UI.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovement
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private Transform fpArms;

        private ActorMovement _movement;
        private CameraLook _cameraLook;
        private FirstPersonCameraSwap _armSwap;
        private SheatheManager _sheathe;
        private PlayerAttack _playerAttack;
        private PlayerInteractionSystem _interactionSystem;
        private InventoryUIManager _inventoryUIManager;

        private PlayerInput _input;
        private InputAction _moveAction;
        private InputAction _increaseSpeedAction;
        private InputAction _lookAction;
        private InputAction _raiseWeaponAction;
        private InputAction _interactAction;
        private InputAction _attackAction;
        private InputAction _showInventoryAction;
        
        public void Start()
        {
            _movement = GetComponent<ActorMovement>();
            _cameraLook = GetComponent<CameraLook>();
            _armSwap = fpArms.GetComponent<FirstPersonCameraSwap>();
            _sheathe = fpArms.GetComponent<SheatheManager>();
            _interactionSystem = GetComponent<PlayerInteractionSystem>();
            _playerAttack = fpArms.GetComponent<PlayerAttack>();
            _inventoryUIManager = GetComponent<InventoryUIManager>();
            
            _inventoryUIManager.UiShown += (sender, args) =>
            {
                _input.actions.Disable();
                _showInventoryAction.Enable();
            };
            
            _inventoryUIManager.UiHidden += (sender, args) =>
            {
                _input.actions.Enable();
            };

            _input = GetComponent<PlayerInput>();
            _moveAction = _input.actions[InputConstants.MoveAction];
            _increaseSpeedAction = _input.actions[InputConstants.ChangeSpeed];
            _lookAction = _input.actions[InputConstants.Look];
            _raiseWeaponAction = _input.actions[InputConstants.RaiseWeapon];
            _interactAction = _input.actions[InputConstants.Interact];
            _attackAction = _input.actions[InputConstants.Attack];
            _showInventoryAction = _input.actions[InputConstants.Inventory];
        }

        public void Update()
        {
            var movementInput = _moveAction.ReadValue<Vector2>();
            var lookInput = _lookAction.ReadValue<Vector2>();
            
            _movement.Move(movementInput);
            _cameraLook.MoveCamera(lookInput);
            _playerAttack.SetMovementInput(movementInput);
            _movement.ChangeSpeed(_increaseSpeedAction.ReadValue<float>() * 0.1f);
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
            
            if (_showInventoryAction.WasPerformedThisFrame())
            {
                _inventoryUIManager.ToggleUI();
            }
        }
    }
}