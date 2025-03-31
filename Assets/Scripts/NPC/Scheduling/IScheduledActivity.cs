using System;

namespace NPC.Scheduling
{
    public interface IScheduledActivity
    {
        public void Execute(NpcController controller);
    }
}