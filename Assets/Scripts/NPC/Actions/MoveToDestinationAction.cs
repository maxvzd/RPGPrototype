using NPC.UtilityBaseClasses;
using UnityEngine;
using UnityEngine.Serialization;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Walk")]
    public class MoveToDestinationAction : NpcAction
    {
        [FormerlySerializedAs("location")] [SerializeField] private Vector2 destination;
        
        public override void Execute(NpcController npcController)
        {
            var asVector3 = new Vector3(destination.x, 0, destination.y);
            npcController.MoveToDestination(asVector3);
        }
    }
}