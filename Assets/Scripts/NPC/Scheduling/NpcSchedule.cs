using UnityEngine;

namespace NPC.Scheduling
{
    public class NpcSchedule : MonoBehaviour
    {
        [SerializeField] private ScheduleItem[] activities;

        private int _currentActivity;
        private NpcController _npcController;

        private void Start()
        {
            WorldTime.TimeUpdated += WorldTimeOnTimeUpdated;
            _npcController = GetComponent<NpcController>();
        }

        private void WorldTimeOnTimeUpdated(int hour, int minute)
        {
            var currentActivity = activities[_currentActivity];
            if (hour >= currentActivity.Hour && minute >= currentActivity.Minute)
            {
                currentActivity.Activity.Execute(_npcController);
                _currentActivity++;
                if (_currentActivity >= activities.Length)
                {
                    _currentActivity = 0;
                }
            }
        }
    }
}