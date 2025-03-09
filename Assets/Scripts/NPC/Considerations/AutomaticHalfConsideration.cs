using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Automatic 0.5")]
    public class AutomaticHalfConsideration : Consideration
    {
        public override float Score() => 0.5f;
    }
}