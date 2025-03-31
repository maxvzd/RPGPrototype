using UnityEngine;

namespace NPC.Scheduling
{
    public abstract class ScheduledActivity : ScriptableObject, IScheduledActivity
    {
        public abstract void Execute(NpcController controller);
    }
}