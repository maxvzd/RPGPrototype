using System;
using FirstPerson;
using Items.Equipment.Sheathing;
using NPC;
using UnityEngine;

namespace Combat
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float attackSensitivity;

        private WeaponSheathing _weaponSheathing;
        private PlayerAnimationEventListener _animationEventHandler;

        private bool _isLeftMouseHeld;
        private bool _isReadyToRelease;
        private bool _isSwingFinished = true;

        private AttackDirection _currentAttackDirection;
        private FirstPersonCombatAnimationManager _combatAnimationHandler;
        private CameraLook _cameraLook;
        private Guid? _cameraLookToken;

        public bool IsWeaponRaised { get; private set; }

        private void Start()
        {
            _weaponSheathing = GetComponent<WeaponSheathing>();
            _animationEventHandler = GetComponent<PlayerAnimationEventListener>();
            _animationEventHandler.SwingFinished += SwingFinished;

            _combatAnimationHandler = GetComponent<FirstPersonCombatAnimationManager>();
            _currentAttackDirection = AttackDirection.None;
            _cameraLook = GetComponentInChildren<CameraLook>();
        }
        
        private void Update()
        {
            if (_weaponSheathing.IsWeaponSheathed) return;
            if (!_isLeftMouseHeld || !_isSwingFinished) return;

            var mouseInput = EntitiesRegistry.Player.Input.MouseInput;

            var newAttackDirection = _currentAttackDirection;
            if (Math.Abs(mouseInput.x) > attackSensitivity || Math.Abs(mouseInput.y) > attackSensitivity)
            {
                if (mouseInput.x > attackSensitivity && HasGreaterMagnitude(mouseInput.x, mouseInput.y)) newAttackDirection = AttackDirection.Left;
                if (mouseInput.x < attackSensitivity && HasGreaterMagnitude(mouseInput.x, mouseInput.y)) newAttackDirection = AttackDirection.Right;
                if (mouseInput.y > attackSensitivity && HasGreaterMagnitude(mouseInput.y, mouseInput.x)) newAttackDirection = AttackDirection.Backward;
                if (mouseInput.y < attackSensitivity && HasGreaterMagnitude(mouseInput.y, mouseInput.x)) newAttackDirection = AttackDirection.Forward;
            }
            
            if (newAttackDirection != _currentAttackDirection)
            {
                _currentAttackDirection = newAttackDirection;
                _combatAnimationHandler.TransitionToWeaponRaisedState(_currentAttackDirection);
            }
        }

        public void StartAttack()
        { 
            if (_weaponSheathing.IsWeaponSheathed) return;
            if(_isLeftMouseHeld) return;
            
            SetSwingState(true);
            _currentAttackDirection = AttackDirection.None;
            EntitiesRegistry.Player.LockOn.LockOnToNearestTarget();
            _cameraLookToken ??= _cameraLook.RegisterCameraLock();
        }
        
        public void ReleaseAttack()
        { 
            if (_weaponSheathing.IsWeaponSheathed) return;
            
            SetSwingState(false);
            if (_cameraLookToken != null)
            {
                _cameraLook.UnRegisterCameraLock(_cameraLookToken.Value);
                _cameraLookToken = null;
            }
        }

        private void SetSwingState(bool isSwinging)
        {
            _combatAnimationHandler.SetShouldReleaseAttack(!isSwinging);
            _isLeftMouseHeld = isSwinging;
            _isSwingFinished = isSwinging;
        }

        private static bool HasGreaterMagnitude(float a, float b)
        {
            return Math.Abs(a) > Math.Abs(b);
        }
        
        private void SwingFinished(object sender, EventArgs e)
        {
            _isSwingFinished = true;
            _combatAnimationHandler.TransitionToIdle();
            IsWeaponRaised = false;
        }
    }
}

public enum AttackDirection
{
    Forward = 0,
    Backward = 1,
    Left = 2,
    Right = 3,
    None = -1
}