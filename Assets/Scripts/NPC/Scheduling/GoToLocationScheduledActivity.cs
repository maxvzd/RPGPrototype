using UnityEngine;

namespace NPC.Scheduling
{
    [CreateAssetMenu(menuName = "NPC/Scheduling/Go To Location")]
    public class GoToLocationActivity : ScheduledActivity
    {
        [SerializeField] private Vector3 location;
        
        public override void Execute(NpcController controller)
        {
            controller.MoveToDestination(location);
        }
    }
}