using System;
using System.Collections.Generic;
using System.Linq;
using Combat;
using Constants;
using FirstPerson;
using Interact;
using Items.Equipment;
using UI.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovement
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private ActorMovement _movement;
        private CameraLook _cameraLook;
        private FirstPersonCameraSwap _armSwap;
        private WeaponPositionManager _weaponPosition;
        private PlayerAttack _playerAttack;
        private PlayerInteractionSystem _interactionSystem;
        private PlayerInventory _inventoryUIManager;

        private PlayerInput _input;
        private InputAction _moveAction;
        private InputAction _increaseSpeedAction;
        private InputAction _lookAction;

        private Dictionary<InputAction, Action> _wasPerformedActions;
        private Dictionary<InputAction, Action> _wasPressedActions;
        private Dictionary<InputAction, Action> _wasCompletedActions;

        public void Start()
        {
            _movement = GetComponent<ActorMovement>();
            _cameraLook = GetComponent<CameraLook>();
            _armSwap = GetComponent<FirstPersonCameraSwap>();
            _weaponPosition = GetComponent<WeaponPositionManager>();
            _interactionSystem = GetComponent<PlayerInteractionSystem>();
            _playerAttack = GetComponent<PlayerAttack>();
            _inventoryUIManager = GetComponent<PlayerInventory>();
            
            _input = GetComponent<PlayerInput>();
            _moveAction = _input.actions[InputConstants.MoveAction];
            _increaseSpeedAction = _input.actions[InputConstants.ChangeSpeed];
            _lookAction = _input.actions[InputConstants.Look];
            var raiseWeaponAction = _input.actions[InputConstants.RaiseWeapon];
            var interactAction = _input.actions[InputConstants.Interact];
            var attackAction = _input.actions[InputConstants.Attack];
            var showInventoryPlayerAction = _input.actions[$"{InputConstants.PlayerActionMap}/{InputConstants.Inventory}"];
            
            var showInventoryUIAction = _input.actions[$"{InputConstants.UIActionMap}/{InputConstants.Inventory}"];
            var dropItemAction = _input.actions[$"{InputConstants.UIActionMap}/{InputConstants.DropItem}"];

            _input.SwitchCurrentActionMap(InputConstants.PlayerActionMap);
            
            _wasPerformedActions = new Dictionary<InputAction, Action>
            {
                {raiseWeaponAction, () => { _armSwap.SwitchArms(); _weaponPosition.SheatheWeapon(); }},
                {interactAction, () => { _interactionSystem.Interact(); }},
                {showInventoryPlayerAction, ToggleUi},
                {showInventoryUIAction, ToggleUi},
                {dropItemAction, () => { _inventoryUIManager.DropSelectedItem(); }},
            };
            
            _wasCompletedActions = new Dictionary<InputAction, Action>
            {
                {attackAction, () => {_playerAttack.ReleaseAttack();}},
            };
            
            _wasPressedActions = new Dictionary<InputAction, Action>
            {
                {attackAction, () => {_playerAttack.HoldAttack();}},
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
            var isInventoryUIShowing = _inventoryUIManager.ToggleUI();
            _input.SwitchCurrentActionMap(isInventoryUIShowing ? InputConstants.UIActionMap : InputConstants.PlayerActionMap);
        }
    }
}