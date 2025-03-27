using System;
using System.Collections;
using Items.Equipment;
using UnityEngine;

namespace Combat
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float attackOpportunityWindow;

        private WeaponPositionManager _weaponPositionManager;
        private PlayerAnimationEventListener _animationEventHandler;

        //private int _attackCounter;
        private Vector2 _movementInput;

        private bool _isLeftMouseHeld;
        private AttackDirection _currentAttackDirection;
        private bool _isReadyToRelease;

        private CombatAnimationStateMachineManager _combatAnimationHandler;
        private Coroutine _waitForPlayerClickRoutine;
        private bool _swingFinished = true;
        private const int MAX_COMBO_COUNT = 3;

        public void HoldAttack()
        {
            if (_weaponPositionManager.IsWeaponSheathed) return;
            if (!_swingFinished) return;
            
            SetIsLeftMouseHeld(true);
            ResetReadyToRelease();
            TransitionToChargeState();

            //if (_attackCounter > 0) return; //Ignore while comboing
            
            //ResetReadyToRelease();
            //TransitionToAttackState();
        }
        
        public void ReleaseAttack()
        {
            if (_weaponPositionManager.IsWeaponSheathed) return;

            SetIsLeftMouseHeld(false);

            if (_isReadyToRelease)
            {
                _combatAnimationHandler.SetShouldReleaseAttack(true);
                //_attackCounter++;
                //StopWaitForPlayerClickRoutine();

                // if (_attackCounter > MAX_COMBO_COUNT - 1)
                // {
                //     EndCombo();
                // }
            }
            else
            {
                _combatAnimationHandler.TransitionToIdle();
                EndCombo();
            }

            _currentAttackDirection = AttackDirection.None;
        }
        
        public void SetMovementInput(Vector2 movementInput)
        {
            _movementInput = movementInput;
        }
        
        private void Start()
        {
            _weaponPositionManager = GetComponent<WeaponPositionManager>();
            _animationEventHandler = GetComponent<PlayerAnimationEventListener>();
            _animationEventHandler.ReadyToAttack += ReadyToAttack;
            _animationEventHandler.SwingFinished += SwingFinished;

            _combatAnimationHandler = GetComponent<CombatAnimationStateMachineManager>();
            _currentAttackDirection = AttackDirection.None;
        }

        // private void Update()
        // {
        //     if (_weaponPositionManager.IsWeaponSheathed) return;
        //
        //     // if (_attackCounter is > 0 and < MAX_COMBO_COUNT && _swingFinished)
        //     // {
        //     //     ResetReadyToRelease();
        //     //     TransitionToAttackState();
        //     // }
        //
        //     if (_isLeftMouseHeld)//&& _attackCounter == 0)
        //     {
        //         TransitionToHoldState();
        //     }
        // }
        
        private void TransitionToChargeState()
        {
            var attackDirection = GetAttackDirection();
            if (_currentAttackDirection == attackDirection) return;

            _currentAttackDirection = attackDirection;
            _combatAnimationHandler.TransitionToChargeState(_currentAttackDirection);
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
            _swingFinished = false;

            //if (_attackCounter > 0)
            //{
                //StartWaitForPlayerClickRoutine();
            //}
        }
        
        private void SwingFinished(object sender, EventArgs e)
        {
            _swingFinished = true;
            //if (_attackCounter == 0) //Combo has ended and been reset
            //{
                _combatAnimationHandler.TransitionToIdle();
            //}
        }

        #region Coroutines
        
        private void StartWaitForPlayerClickRoutine()
        {
            StopWaitForPlayerClickRoutine();
            _waitForPlayerClickRoutine = StartCoroutine(WaitForPlayerClick());
        }

        private void StopWaitForPlayerClickRoutine()
        {
            if (_waitForPlayerClickRoutine is null) return;
            
            StopCoroutine(_waitForPlayerClickRoutine);
            _waitForPlayerClickRoutine = null;
        }

        private IEnumerator WaitForPlayerClick()
        {
            yield return new WaitForSeconds(attackOpportunityWindow);

            EndCombo();
            _combatAnimationHandler.TransitionToIdle();
        }
        
        #endregion

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

        private void SetIsLeftMouseHeld(bool isLeftMouseHeld)
        {
            _isLeftMouseHeld = isLeftMouseHeld;
            _combatAnimationHandler.SetIsLeftMouseButtonDown(_isLeftMouseHeld);
        }

        private void ResetReadyToRelease()
        {
            _isReadyToRelease = false;
            _combatAnimationHandler.SetShouldReleaseAttack(_isReadyToRelease);
        }

        private void EndCombo()
        {
            //_attackCounter = 0;
            _swingFinished = true;
            _currentAttackDirection = AttackDirection.None;
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