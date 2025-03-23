using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Combat
{
    public class CombatAnimationStateMachineManager : MonoBehaviour
    {
        private Animator _animator;

        private readonly IReadOnlyDictionary<AttackDirection, int> _chargeAttacks = new Dictionary<AttackDirection, int>
        {
            { AttackDirection.Backward, AnimatorConstants.ChargeAttackDownState },
            { AttackDirection.Forward, AnimatorConstants.ChargeAttackStabState },
            { AttackDirection.Left, AnimatorConstants.ChargeAttackLeftState },
            { AttackDirection.Right, AnimatorConstants.ChargeAttackRightState },
            { AttackDirection.None, AnimatorConstants.CombatReadyState }
        };
        
        private readonly IReadOnlyDictionary<AttackDirection, int> _holdAttacks = new Dictionary<AttackDirection, int>
        {
            { AttackDirection.Backward, AnimatorConstants.HoldAttackDownState },
            { AttackDirection.Forward, AnimatorConstants.HoldAttackStabState },
            { AttackDirection.Left, AnimatorConstants.HoldAttackLeftState },
            { AttackDirection.Right, AnimatorConstants.HoldAttackRightState },
            { AttackDirection.None, AnimatorConstants.HoldAttackLeftState }
        };

        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            _animator.SetFloat(AnimatorConstants.AttackSpeedModifier, .7f);
            _animator.SetFloat(AnimatorConstants.RaiseWeaponSpeedModifier, 1f);
        }

        public void TransitionToIdle()
        {
            _animator.CrossFadeInFixedTime(AnimatorConstants.CombatReadyState, 0.3f, 1, 0f, 0);
            _animator.CrossFadeInFixedTime(AnimatorConstants.CombatReadyState, 0.3f, 2, 0f, 0);
        }

        public void TransitionAttackStateToState(AttackDirection attackDirection)
        {
            var state = _chargeAttacks[attackDirection];
            _animator.CrossFadeInFixedTime(state, 0.2f, 1, 0f, 0.5f);
            _animator.CrossFadeInFixedTime(state, 0.2f, 2, 0f, 0.5f);
        }

        public void TransitionToHoldState(AttackDirection attackDirection)
        {
            var state = _holdAttacks[attackDirection];
            _animator.CrossFadeInFixedTime(state, 0.3f, 1, 0, 0);
            _animator.CrossFadeInFixedTime(state, 0.3f, 2, 0, 0);
        }

        public void SetShouldReleaseAttack(bool shouldReleaseAttack)
        {
            _animator.SetBool(AnimatorConstants.ShouldReleaseAttack, shouldReleaseAttack);
        }
        
        public void SetIsLeftMouseButtonDown(bool isLeftMouseButtonDown)
        {
            _animator.SetBool(AnimatorConstants.IsLeftMouseButtonDown, isLeftMouseButtonDown);
        }
    }
}