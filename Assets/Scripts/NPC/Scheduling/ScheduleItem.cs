using System;
using NPC.UtilityBaseClasses;
using UnityEngine;
using UnityEngine.Serialization;

namespace NPC.Scheduling
{
    [Serializable]
    public class ScheduleItem
    {
        [SerializeField] [Range(0, 23)] private int hour;
        [SerializeField] [Range(0, 60)] private int minute;
        [FormerlySerializedAs("scheduledActivity")] [SerializeField] private NpcAction scheduledAction;

        public int Hour => hour;
        public int Minute => minute;
        public NpcAction Action => scheduledAction;
    }
}