using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations.SpeechConsiderations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Speech Considerations/Disposition")]
    public class Disposition : Consideration
    {
        [SerializeField] private AnimationCurve dispositionCurve; 

        public override float Score(ConsiderationContextGenerator contextGenerator)
        {
            var speechContext = contextGenerator.GetSpeechContext();
            return dispositionCurve.Evaluate(speechContext.Disposition);
        }
    }
}