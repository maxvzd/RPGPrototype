using UnityEngine;

namespace Constants
{
    public static class AnimatorConstants
    {
        #region Parameters
                
        public static readonly int Vertical = Animator.StringToHash("Vertical");
        public static readonly int Horizontal = Animator.StringToHash("Horizontal");
        public static readonly int WeaponSheathed = Animator.StringToHash("WeaponSheathed");
        public static readonly int JumpTrigger = Animator.StringToHash("JumpTrigger");
        public static readonly int AttackSpeedModifier = Animator.StringToHash("AttackSpeedModifier");
        public static readonly int RaiseWeaponSpeedModifier = Animator.StringToHash("RaiseWeaponSpeedModifier");
        public static readonly int ShouldReleaseAttack = Animator.StringToHash("ShouldReleaseAttack");
        public static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        public static readonly int DropRightItem = Animator.StringToHash("DropRightItem");
        
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
        public static readonly int CombatReadyState = Animator.StringToHash("CombatReady");
        public static readonly int TurnLeftState = Animator.StringToHash("TurnLeft");
        public static readonly int TurnRightState = Animator.StringToHash("TurnRight");
        public static readonly int ShouldTurnLeft = Animator.StringToHash("ShouldTurnLeft");
        public static readonly int ShouldTurnRight = Animator.StringToHash("ShouldTurnRight");
        
        public static readonly int UnsheatheState = Animator.StringToHash("Unsheathe");
        public static readonly int SheatheState = Animator.StringToHash("Sheathe");
        public static readonly int CombatReadyWalkState = Animator.StringToHash("CombatReadyWalk");
        public static readonly int DropItemState = Animator.StringToHash("DropItem");
        
        #endregion
    }
}