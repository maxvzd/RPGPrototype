using System;
using System.Collections.Generic;
using System.Linq;
using Combat;
using Constants;
using FirstPerson;
using Interact;
using Items.Equipment;
using PlayerMovement;
using UI.Container;
using UI.Dialogue;
using UI.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private ActorMovement _movement;
        private CameraLook _cameraLook;
        private FirstPersonCameraSwap _armSwap;
        private PlayerAttack _playerAttack;

        private PlayerInput _input;
        private InputAction _moveAction;
        private InputAction _increaseSpeedAction;
        private InputAction _lookAction;

        private Dictionary<InputAction, Action> _wasPerformedActions;
        private Dictionary<InputAction, Action> _wasPressedActions = new();
        private Dictionary<InputAction, Action> _wasCompletedActions;

        private readonly PlayerInputState _state = new();
        private Dictionary<InputAction, Action> _isPressedActions;
        public IPlayerInputState State => _state;
        
        public void Start()
        {
            _movement = GetComponent<ActorMovement>();
            _cameraLook = GetComponent<CameraLook>();
            var weaponPosition = GetComponent<WeaponPositionManager>();
            var interactionSystem = GetComponent<PlayerInteractionSystem>();
            _playerAttack = GetComponent<PlayerAttack>();
            var inventoryUIManager = GetComponent<PlayerInventoryUiManager>();
            var dialogueManager = GetComponent<DialogueManager>();
            var containerManager = GetComponent<ContainerUiManager>();
            var jumper = GetComponent<JumpBehaviour>();

            _input = GetComponent<PlayerInput>();
            _moveAction = _input.actions[InputConstants.MoveAction];
            _increaseSpeedAction = _input.actions[InputConstants.ChangeSpeed];
            _lookAction = _input.actions[InputConstants.Look];
            var raiseWeaponAction = _input.actions[InputConstants.RaiseWeapon];
            var interactAction = _input.actions[InputConstants.Interact];
            var attackAction = _input.actions[InputConstants.Attack];
            var jumpAction = _input.actions[InputConstants.Jump];
            var showInventoryPlayerAction = _input.actions[$"{InputConstants.PlayerActionMap}/{InputConstants.Inventory}"];
            var showInventoryUIAction = _input.actions[$"{InputConstants.UIActionMap}/{InputConstants.Inventory}"];
            var hideInventoryUIAction = _input.actions[$"{InputConstants.UIActionMap}/{InputConstants.Interact}"];
            var dropItemAction = _input.actions[$"{InputConstants.UIActionMap}/{InputConstants.DropItem}"];

            _input.SwitchCurrentActionMap(InputConstants.PlayerActionMap);

            _wasPerformedActions = new Dictionary<InputAction, Action>
            {
                { raiseWeaponAction, () => { weaponPosition.SheatheWeapon(); } },
                { interactAction, () => { interactionSystem.Interact(); } },
                { showInventoryPlayerAction, inventoryUIManager.ShowUI },
                { hideInventoryUIAction, () =>
                    {
                        inventoryUIManager.HideUI();
                        dialogueManager.HideUI();
                        containerManager.HideUI();
                    }
                },
                { showInventoryUIAction, () =>
                    {
                        inventoryUIManager.HideUI();
                        dialogueManager.HideUI();
                        containerManager.HideUI();
                    }
                },
                { dropItemAction, () => { inventoryUIManager.DropSelectedItem(); } },
                { jumpAction, () => { jumper.Jump(); } },
            };

            _wasCompletedActions = new Dictionary<InputAction, Action>
            {
                { attackAction, () => { _playerAttack.ReleaseAttack(); } },
            };
            
            _isPressedActions = new Dictionary<InputAction, Action>
            {
                { attackAction, () => { _playerAttack.StartAttack(); } },
            };

            inventoryUIManager.UiHidden += EnableRegularActions;
            inventoryUIManager.UiShown += EnableUiActions;
            dialogueManager.UiHidden += EnableRegularActions;
            dialogueManager.UiShown += EnableUiActions;
            containerManager.UiHidden += EnableRegularActions;
            containerManager.UiShown += EnableUiActions;
        }

        public void Update()
        {
            _state.MovementInput = _moveAction.ReadValue<Vector2>();
            _state.MouseInput =_lookAction.ReadValue<Vector2>();

            _movement.Move(_state.MovementInput);
            _cameraLook.MoveCamera(_state.MouseInput);
            _movement.ChangeSpeed(_increaseSpeedAction.ReadValue<float>() * 0.1f);
            _cameraLook.TiltCamera(_state.MovementInput.x);

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
            
            foreach (var action in
                     _isPressedActions.Where(action => action.Key.IsPressed()))
            {
                action.Value.Invoke();
            }
        }

        private void EnableUiActions(object sender, EventArgs e)
        {
            _input.SwitchCurrentActionMap(InputConstants.UIActionMap);
        }

        private void EnableRegularActions(object sender, EventArgs e)
        {
            _input.SwitchCurrentActionMap(InputConstants.PlayerActionMap);
        }
    }
}