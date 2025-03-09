using System;
using System.Collections.Generic;
using System.Linq;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC
{
    public class DecisionMaker : MonoBehaviour
    {
        [SerializeField] private NpcAction[] availableActions;
        private NpcController _npcController;

        public void Start()
        {
            _npcController = GetComponent<NpcController>();
        }

        private void Update()
        {
            if (_npcController.IsIdle)
            {
                CalculateDecision();
            }
        }

        public void CalculateDecision()
        {
            var scores = new Dictionary<float, NpcAction>();
            foreach (var action in availableActions)
            {
                var score = action.CalculateScore();
                scores.Add(score, action);
            }

            scores.OrderByDescending(x => x.Key).First().Value.Execute(_npcController);
        }
    }
}
