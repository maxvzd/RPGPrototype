using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Constant Value")]
    public class ConstantValueConsideration : Consideration
    {
        [SerializeField] private float value;

        public override float Score(ConsiderationContextGenerator contextGenerator) => value;
    }
}