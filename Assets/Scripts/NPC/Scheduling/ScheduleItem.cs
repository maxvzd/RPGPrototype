using UnityEngine;

namespace NPC.Scheduling
{
    [CreateAssetMenu(menuName = "NPC/Scheduling/Schedule Item")]
    public class ScheduleItem : ScriptableObject
    {
        [SerializeField] [Range(0, 23)] private int hour;
        [SerializeField] [Range(0, 60)] private int minute;
        [SerializeField] private ScheduledActivity scheduledActivity;

        public int Hour => hour;
        public int Minute => minute;
        public ScheduledActivity Activity => scheduledActivity;

    }
}