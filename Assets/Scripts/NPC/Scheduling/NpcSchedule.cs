using System.Linq;
using UnityEngine;

namespace NPC.Scheduling
{
    public class NpcSchedule : MonoBehaviour
    {
        [SerializeField] private ScheduleItem[] activities;

        private ScheduleItem _currentActivity;
        private NpcDecisionMaker _npcDecisionMaker;

        public ScheduleItem CurrentActivity => _currentActivity;

        private bool _isNpcFollowingSchedule;


        private void Start()
        {
            WorldTime.TimeUpdated += WorldTimeOnTimeUpdated;
            _npcDecisionMaker = GetComponent<NpcDecisionMaker>();
        }

        private void WorldTimeOnTimeUpdated(int hour, int minute)
        {
            var scheduleItem = activities.FirstOrDefault(x => x.Hour == hour && x.Minute == minute);
            if (scheduleItem is null) return;
            
            if (_currentActivity is not null)
                _npcDecisionMaker.UnScheduleItem(_currentActivity);
            
            _currentActivity = scheduleItem;
            _npcDecisionMaker.ScheduleItem(_currentActivity);
        }
    }
}