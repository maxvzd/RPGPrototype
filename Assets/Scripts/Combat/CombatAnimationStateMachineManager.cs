using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Combat
{
    public class CombatAnimationStateMachineManager : MonoBehaviour
    {
        private Animator _animator;

        private readonly IReadOnlyDictionary<AttackDirection, int> _weaponRaisedStates = new Dictionary<AttackDirection, int>
        {
            { AttackDirection.Backward, AnimatorConstants.ChargeAttackDownState },
            { AttackDirection.Forward, AnimatorConstants.ChargeAttackStabState },
            { AttackDirection.Left, AnimatorConstants.ChargeAttackLeftState },
            { AttackDirection.Right, AnimatorConstants.ChargeAttackRightState },
            { AttackDirection.None, AnimatorConstants.CombatReadyState }
        };

        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            _animator.SetFloat(AnimatorConstants.AttackSpeedModifier, 1f);
            _animator.SetFloat(AnimatorConstants.RaiseWeaponSpeedModifier, 1f);
        }

        public void TransitionToIdle()
        {           
            _animator.CrossFadeInFixedTime(AnimatorConstants.CombatReadyState, .5f, 1, 0, 0);
        }

        public void TransitionToWeaponRaisedState(AttackDirection attackDirection)
        {
            var state = _weaponRaisedStates[attackDirection];
            _animator.CrossFadeInFixedTime(state, .4f, 1, 0, 0);
        }

        public void SetShouldReleaseAttack(bool shouldReleaseAttack)
        {
            _animator.SetBool(AnimatorConstants.ShouldReleaseAttack, shouldReleaseAttack);
        }
    }
}