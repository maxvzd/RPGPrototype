using NPC.UtilityBaseClasses;
using NPC.UtilityBaseClasses.Contexts;
using UnityEngine;

namespace NPC.Considerations.SpeechConsiderations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Speech Considerations/Disposition")]
    public class Disposition : Consideration<SpeechConsiderationContext>
    {
        [SerializeField] private AnimationCurve dispositionCurve; 
        
        public override float Score(SpeechConsiderationContext context)
        {
            return dispositionCurve.Evaluate(context.Disposition);
        }
    }
}