using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Combat
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator _animator;
        //private SheatheManager _sheatheManager;

        private int _attackCounter;
        private Vector2 _movementInput;

        private bool _isLeftMouseHeld;
        private AttackDirection _currentAttackDirection;

        private readonly IReadOnlyDictionary<AttackDirection, int> _chargeAttacks = new Dictionary<AttackDirection, int>
        {
            { AttackDirection.Forward, AnimatorConstants.ChargeAttackStabState },
            { AttackDirection.Backward, AnimatorConstants.ChargeAttackDownState },
            { AttackDirection.Left, AnimatorConstants.ChargeAttackLeftState },
            { AttackDirection.Right, AnimatorConstants.ChargeAttackRightState }
        };

        private void Start()
        {
            _animator = GetComponent<Animator>();
            //_sheatheManager = GetComponent<SheatheManager>();
            _animator.SetFloat(AnimatorConstants.AttackSpeedModifier, 1f);
            _animator.SetFloat(AnimatorConstants.RaiseWeaponSpeedModifier, 1f);
        }

        private void Update()
        {
            if (_attackCounter > 0)
            {
                if (IsCurrentAnimatorState(AnimatorConstants.AttackFinishedState))
                {
                    SetAttackAnims();
                    PlayAttackAnim();
                }
            }

            if (_isLeftMouseHeld)
            {
                SetAttackAnims();
            }
        }

        private bool IsCurrentAnimatorState(int animatorHash)
        {
            return _animator.GetCurrentAnimatorStateInfo(0).shortNameHash == animatorHash;
        }

        public void SetMovementInput(Vector2 movementInput)
        {
            _movementInput = movementInput;
        }

        public void HoldAttack()
        {
            //if (_sheatheManager.IsWeaponSheathed || _attackCounter != 0) return;
            
            _isLeftMouseHeld = true;
            _animator.SetBool(AnimatorConstants.IsLeftMouseButtonDown, _isLeftMouseHeld);
            
            SetAttackAnims();

            PlayAttackAnim();
        }

        private void PlayAttackAnim()
        {
            var anim = _chargeAttacks[_currentAttackDirection];
            _animator.Play(anim);
        }

        private void SetAttackAnims()
        {
            var attackDirection = GetAttackDirection();
            if (_currentAttackDirection == attackDirection) return;

            _currentAttackDirection = attackDirection;
            _animator.SetInteger(AnimatorConstants.AttackDirection, (int)_currentAttackDirection);
        }

        private AttackDirection GetAttackDirection()
        {
            var movement = _movementInput;
            var vertical = movement.y;
            var horizontal = movement.x;

            switch (vertical)
            {
                case > 0:
                    return AttackDirection.Forward;
                case < 0:
                    return AttackDirection.Backward;
            }

            return horizontal > 0 ? AttackDirection.Right : AttackDirection.Left;
        }

        public void ReleaseAttack()
        {
            _isLeftMouseHeld = false;
            _animator.SetBool(AnimatorConstants.IsLeftMouseButtonDown, _isLeftMouseHeld);
            
            // _attackCounter++;
            // if (_attackCounter > 2)
            // {
            //     _attackCounter = 0;
            // }
        }
    }
}

enum AttackDirection
{
    Forward = 0,
    Backward = 1,
    Left = 2,
    Right = 3
}