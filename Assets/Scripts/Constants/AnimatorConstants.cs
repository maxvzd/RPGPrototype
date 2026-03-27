using UnityEngine;

namespace Constants
{
    public static class AnimatorConstants
    {
        #region Parameters
                
        public static readonly int Vertical = Animator.StringToHash("Vertical");
        public static readonly int Horizontal = Animator.StringToHash("Horizontal");
        public static readonly int WeaponSheathed = Animator.StringToHash("WeaponSheathed");
        public static readonly int TurnRightTrigger = Animator.StringToHash("TurnRightTrigger");
        public static readonly int TurnLeftTrigger = Animator.StringToHash("TurnLeftTrigger");
        public static readonly int JumpTrigger = Animator.StringToHash("JumpTrigger");
        public static readonly int AttackSpeedModifier = Animator.StringToHash("AttackSpeedModifier");
        public static readonly int RaiseWeaponSpeedModifier = Animator.StringToHash("RaiseWeaponSpeedModifier");
        public static readonly int ShouldReleaseAttack = Animator.StringToHash("ShouldReleaseAttack");
        public static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        
        #endregion
        
        #region states
        
        public static readonly int HoldAttackStabState = Animator.StringToHash("HoldAttackStab");
        public static readonly int HoldAttackLeftState = Animator.StringToHash("HoldAttackLeft");
        public static readonly int HoldAttackRightState = Animator.StringToHash("HoldAttackRight");
        public static readonly int HoldAttackDownState = Animator.StringToHash("HoldAttackDown");
        public static readonly int ChargeAttackRightState = Animator.StringToHash("ChargeAttackRight");
        public static readonly int ChargeAttackLeftState = Animator.StringToHash("ChargeAttackLeft");
        public static readonly int ChargeAttackStabState = Animator.StringToHash("ChargeAttackStab");
        public static readonly int ChargeAttackDownState = Animator.StringToHash("ChargeAttackDown");
        public static readonly int CombatReadyState = Animator.StringToHash("Combat Ready");
        public static readonly int TurnLeftState = Animator.StringToHash("TurnLeft");
        public static readonly int TurnRightState = Animator.StringToHash("TurnRight");
        public static readonly int ShouldTurnLeft = Animator.StringToHash("ShouldTurnLeft");
        public static readonly int ShouldTurnRight = Animator.StringToHash("ShouldTurnRight");
        
        #endregion
    }
}