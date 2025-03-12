using NPC.UtilityBaseClasses;
using NPC.UtilityBaseClasses.Contexts;
using UnityEngine;

namespace NPC.Considerations.SpeechConsiderations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Speech Considerations/Disposition")]
    public class Disposition : Consideration<SpeechContext>
    {
        [SerializeField] private AnimationCurve dispositionCurve; 
        
        public override float Score(SpeechContext context)
        {
            return dispositionCurve.Evaluate(context.Disposition);
        }
    }
}