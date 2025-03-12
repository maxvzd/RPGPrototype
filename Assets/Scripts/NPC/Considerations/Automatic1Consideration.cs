using NPC.UtilityBaseClasses;
using NPC.UtilityBaseClasses.Contexts;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Automatic 1")]
    public class Automatic1Consideration : Consideration<GenericContext>
    {
        public override float Score(GenericContext context)=> 1f;
    }
}