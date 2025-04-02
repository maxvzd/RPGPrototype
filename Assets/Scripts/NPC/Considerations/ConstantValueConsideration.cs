using NPC.UtilityBaseClasses;
using NPC.UtilityBaseClasses.Contexts;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Constant Value")]
    public class ConstantValueConsideration : Consideration<GenericContext>
    {
        [SerializeField] private float value;

        public override float Score(GenericContext context) => value;
    }
}