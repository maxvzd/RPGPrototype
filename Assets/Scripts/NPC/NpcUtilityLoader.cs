using System;
using System.Collections.Generic;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC
{
    public class NpcUtilityLoader : MonoBehaviour
    {
        public static NpcUtilityLoader Instance { get; private set; }
        public IReadOnlyDictionary<string, UtilityGoal> Goals => _goals;
    
        private readonly Dictionary<string, UtilityGoal> _goals = new();
    
        private void Awake()
        {
            LoadScriptableObjects();
        
            if (Instance is null)
            {
                Instance = this;
            }
            else
            {
                throw new Exception($"More than one instance of {nameof(NpcUtilityLoader)} detected.");
            }
        }

        private void LoadScriptableObjects()
        {
            var goals = Resources.LoadAll<UtilityGoal>("Goals");

            foreach (var goal in goals)
            {
                _goals.Add(goal.name, goal);
            }
        }
    }
}
