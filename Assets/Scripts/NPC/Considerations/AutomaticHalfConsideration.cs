using NPC.UtilityBaseClasses;
using NPC.UtilityBaseClasses.Contexts;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Automatic 0.5")]
    public class AutomaticHalfConsideration : Consideration<GenericContext>
    {
        public override float Score(GenericContext context) => 0.5f;
    }
}