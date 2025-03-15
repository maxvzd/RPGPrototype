using System;
using System.Collections;
using Constants;
using UnityEngine;

namespace Combat
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float attackOpportunityWindow;

        private Animator _animator;
        private SheatheManager _sheatheManager;
        private FpArmsAnimationEventHandler _animationEventHandler;

        private int _attackCounter;
        private Vector2 _movementInput;

        private bool _isLeftMouseHeld;
        private AttackDirection _currentAttackDirection;
        private bool _isReadyToRelease;

        private IEnumerator _waitForPlayerClickRoutine;
        private CombatAnimationStateMachineManager _combatAnimationHandler;
        private const int MAX_COMBO_COUNT = 3;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _sheatheManager = GetComponent<SheatheManager>();
            _animationEventHandler = GetComponent<FpArmsAnimationEventHandler>();
            _animationEventHandler.ReadyToAttack += ReadyToAttack;

            _combatAnimationHandler = GetComponent<CombatAnimationStateMachineManager>();
            _currentAttackDirection = AttackDirection.None;
        }
        
        
        private void Update()
        {
            if (_sheatheManager.IsWeaponSheathed) return;
            
            if(_attackCounter is > 0 and < MAX_COMBO_COUNT && IsCurrentAnimatorState(AnimatorConstants.AttackFinishedState))
            {
                ResetReadyToRelease();
                TransitionToAttackState();
            }
            
            if (_isLeftMouseHeld && _attackCounter == 0)
            {
                TransitionToHoldState();
            }
        }

        private void TransitionToHoldState()
        {
            var attackDirection = GetAttackDirection();
            if (_currentAttackDirection == attackDirection) return;

            _currentAttackDirection = attackDirection;
            _combatAnimationHandler.TransitionToHoldState(_currentAttackDirection);
        }

        private void ReadyToAttack(object sender, EventArgs e)
        {
            _isReadyToRelease = true;
            
            if (_attackCounter > 0)
            {
                StartWaitForPlayerClickRoutine();
            }
        }

        private void StartWaitForPlayerClickRoutine()
        {
            StopWaitForPlayerClickRoutine();

            _waitForPlayerClickRoutine = WaitForPlayerClick();
            StartCoroutine(_waitForPlayerClickRoutine);
        }

        private void StopWaitForPlayerClickRoutine()
        {
            if (_waitForPlayerClickRoutine is null) return;
            StopCoroutine(_waitForPlayerClickRoutine);
        }

        private IEnumerator WaitForPlayerClick()
        {
            yield return new WaitForSeconds(attackOpportunityWindow);

            //EndCombo();
        }

        private void TransitionToAttackState()
        {
            var attackDirection = GetAttackDirection();
            if (_currentAttackDirection == attackDirection) return;

            _currentAttackDirection = attackDirection;
            _combatAnimationHandler.TransitionAttackStateToState(_currentAttackDirection);
        }

        private bool IsCurrentAnimatorState(int animatorHash)
        {
            return _animator.GetCurrentAnimatorStateInfo(0).shortNameHash == animatorHash;
        }

        public void SetMovementInput(Vector2 movementInput)
        {
            _movementInput = movementInput;
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

        public void HoldAttack()
        {
            if (_sheatheManager.IsWeaponSheathed) return;

            SetIsLeftMouseHeld(true);

            if (_attackCounter > 0) return; //Ignore while comboing
            ResetReadyToRelease();
            TransitionToAttackState();
        }

        private void SetIsLeftMouseHeld(bool isLeftMouseHeld)
        {
            _isLeftMouseHeld = isLeftMouseHeld;
            _animator.SetBool(AnimatorConstants.IsLeftMouseButtonDown, _isLeftMouseHeld);
        }

        private void ResetReadyToRelease()
        {
            _isReadyToRelease = false;
            _animator.SetBool(AnimatorConstants.ShouldReleaseAttack, _isReadyToRelease);
        }


        public void ReleaseAttack()
        {
            if (_sheatheManager.IsWeaponSheathed) return;

            SetIsLeftMouseHeld(false);

            if (_isReadyToRelease)
            {
                _animator.SetBool(AnimatorConstants.ShouldReleaseAttack, true);
                _attackCounter++;
                if (_attackCounter > MAX_COMBO_COUNT - 1)
                {
                    EndCombo();
                }
            }
            else
            {
                _combatAnimationHandler.TransitionToIdle();
                EndCombo();
            }

            _currentAttackDirection = AttackDirection.None;
        }

        private void EndCombo()
        {
            _attackCounter = 0;
            StopAllCoroutines();
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