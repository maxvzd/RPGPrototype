using System;
using System.Collections.Generic;
using System.Linq;
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
        private InputAction _showInventoryPlayerAction;
        private InputAction _showInventoryUIAction;
        private InputAction _dropItemAction;

        private Dictionary<InputAction, Action> _wasPerformedActions;
        private Dictionary<InputAction, Action> _wasPressedActions;
        private Dictionary<InputAction, Action> _wasCompletedActions;

        public void Start()
        {
            _movement = GetComponent<ActorMovement>();
            _cameraLook = GetComponent<CameraLook>();
            _armSwap = fpArms.GetComponent<FirstPersonCameraSwap>();
            _sheathe = fpArms.GetComponent<SheatheManager>();
            _interactionSystem = GetComponent<PlayerInteractionSystem>();
            _playerAttack = fpArms.GetComponent<PlayerAttack>();
            _inventoryUIManager = GetComponent<InventoryUIManager>();
            
            // _inventoryUIManager.UiShown += (sender, args) =>
            // {
            //     _input.actions.Disable();
            //     _showInventoryPlayerAction.Enable();
            // };
            //
            // _inventoryUIManager.UiHidden += (sender, args) =>
            // {
            //     _input.actions.Enable();
            // };

            _input = GetComponent<PlayerInput>();
            _moveAction = _input.actions[InputConstants.MoveAction];
            _increaseSpeedAction = _input.actions[InputConstants.ChangeSpeed];
            _lookAction = _input.actions[InputConstants.Look];
            _raiseWeaponAction = _input.actions[InputConstants.RaiseWeapon];
            _interactAction = _input.actions[InputConstants.Interact];
            _attackAction = _input.actions[InputConstants.Attack];
            _showInventoryPlayerAction = _input.actions[$"{InputConstants.PlayerActionMap}/{InputConstants.Inventory}"];
            
            _showInventoryUIAction = _input.actions[$"{InputConstants.UIActionMap}/{InputConstants.Inventory}"];
            _dropItemAction = _input.actions[$"{InputConstants.UIActionMap}/{InputConstants.DropItem}"];

            _input.SwitchCurrentActionMap(InputConstants.PlayerActionMap);
            
            _wasPerformedActions = new Dictionary<InputAction, Action>
            {
                {_raiseWeaponAction, () => { _armSwap.SwitchArms(); _sheathe.SheatheWeapon(); }},
                {_interactAction, () => { _armSwap.SwitchArms(); _interactionSystem.Interact(); }},
                {_showInventoryPlayerAction, ToggleUi},
                {_showInventoryUIAction, ToggleUi},
                {_dropItemAction, () => { _inventoryUIManager.DropSelectedItem(); }},
            };
            
            _wasCompletedActions = new Dictionary<InputAction, Action>
            {
                {_attackAction, () => {_playerAttack.ReleaseAttack();}},
            };
            
            _wasPressedActions = new Dictionary<InputAction, Action>
            {
                {_attackAction, () => {_playerAttack.HoldAttack();}},
            };
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

            foreach (var action in 
                     _wasPerformedActions.Where(action => action.Key.WasPerformedThisFrame()))
            {
                action.Value.Invoke();
            }
            
            foreach (var action in 
                     _wasPressedActions.Where(action => action.Key.WasPressedThisFrame()))
            {
                action.Value.Invoke();
            }
            
            foreach (var action in 
                     _wasCompletedActions.Where(action => action.Key.WasCompletedThisFrame()))
            {
                action.Value.Invoke();
            }
        }

        private void ToggleUi()
        {
            Debug.Log("Toggling");
            var isInventoryUIShowing = _inventoryUIManager.ToggleUI();
            _input.SwitchCurrentActionMap(isInventoryUIShowing ? InputConstants.UIActionMap : InputConstants.PlayerActionMap);
        }
    }
}