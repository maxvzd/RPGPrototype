using UnityEngine;

namespace Constants
{
    public static class AnimatorConstants
    {
        public static readonly int Vertical = Animator.StringToHash("Vertical");
        public static readonly int Horizontal = Animator.StringToHash("Horizontal");        
        public static readonly int SheatheTrigger = Animator.StringToHash("SheatheTrigger");        
        public static readonly int UnSheatheTrigger = Animator.StringToHash("UnSheatheTrigger");
        public static readonly int TurnRightTrigger = Animator.StringToHash("TurnRightTrigger");
        public static readonly int TurnLeftTrigger = Animator.StringToHash("TurnLeftTrigger");
    }
}