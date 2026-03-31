using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace FirstPerson
{
    public class FirstPersonCombatAnimationManager : MonoBehaviour
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
            _animator.CrossFadeInFixedTime(AnimatorConstants.CombatReadyState, .5f, (int)FirstPersonAnimationLayers.RightArm, 0, 0);
            _animator.CrossFadeInFixedTime(AnimatorConstants.CombatReadyState, .5f, (int)FirstPersonAnimationLayers.LeftArm, 0, 0);
        }

        public void TransitionToWeaponRaisedState(AttackDirection attackDirection)
        {
            var state = _weaponRaisedStates[attackDirection];
            _animator.CrossFadeInFixedTime(state, .4f, (int)FirstPersonAnimationLayers.RightArm, 0, 0);
            _animator.CrossFadeInFixedTime(state, .4f, (int)FirstPersonAnimationLayers.LeftArm, 0, 0);
        }

        public void SetShouldReleaseAttack(bool shouldReleaseAttack)
        {
            _animator.SetBool(AnimatorConstants.ShouldReleaseAttack, shouldReleaseAttack);
        }
        
        public void MoveToUnsheatheState(FirstPersonAnimationLayers layer)
        {
            _animator.CrossFadeInFixedTime(AnimatorConstants.UnsheatheState, .5f, (int)layer, 0, 0);
        }
        
        public void MoveToSheatheState(FirstPersonAnimationLayers layer)
        {
            _animator.CrossFadeInFixedTime(AnimatorConstants.SheatheState, .5f, (int)layer, 0, 0);
        }

        public void MoveToDropItemState(FirstPersonAnimationLayers layer)
        {
            _animator.CrossFadeInFixedTime(AnimatorConstants.DropItemState, .5f, (int)layer, 0, 0);
        }

        public void TransitionToBlock()
        {
            _animator.SetBool(AnimatorConstants.BlockHeld, true);
            _animator.CrossFadeInFixedTime(AnimatorConstants.BlockState, .2f, (int)FirstPersonAnimationLayers.LeftArm, 0, 0);
            _animator.CrossFadeInFixedTime(AnimatorConstants.BlockState, .2f, (int)FirstPersonAnimationLayers.RightArm, 0, 0);
        }

        public void ReleaseBlock()
        {
            _animator.SetBool(AnimatorConstants.BlockHeld, false);
        }
    }

    public enum FirstPersonAnimationLayers
    {
        Base = 0,
        RightArm = 2,
        LeftArm = 3
    }
}