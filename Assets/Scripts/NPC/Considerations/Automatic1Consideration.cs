using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Considerations
{
    [CreateAssetMenu(menuName = "NPC/Considerations/Automatic 1")]
    public class Automatic1Consideration : Consideration
    {
        public override float Score() => 1f;
    }
}