using System.Linq;
using UnityEngine;

namespace NPC.Scheduling
{
    public class NpcSchedule : MonoBehaviour
    {
        [SerializeField] private ScheduleItem[] activities;

        private ScheduledActivity _currentActivity;
        private NpcController _npcController;

        public ScheduledActivity CurrentActivity => _currentActivity;

        private bool _isNpcFollowingSchedule;
        

        private void Start()
        {
            WorldTime.TimeUpdated += WorldTimeOnTimeUpdated;
            _npcController = GetComponent<NpcController>();
        }

        public void FollowSchedule()
        {
            _isNpcFollowingSchedule = true;
        }

        public void StopFollowingSchedule()
        {
            _isNpcFollowingSchedule = false;
        }

        private void WorldTimeOnTimeUpdated(int hour, int minute)
        {
            var scheduleItem = activities.FirstOrDefault(x => x.Hour == hour && x.Minute == minute);
            if (scheduleItem is not null)
            {
                _currentActivity = scheduleItem.Activity;
            }

            if (_currentActivity is not null && _isNpcFollowingSchedule)
            {
                _currentActivity.Execute(_npcController);
            }
        }
    }
}