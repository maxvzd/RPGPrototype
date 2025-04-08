using System;
using System.Collections.Generic;
using System.Linq;
using NPC.Scheduling;
using NPC.UtilityBaseClasses;
using NPC.UtilityBaseClasses.Contexts;
using UnityEngine;

namespace NPC
{
    public class NpcDecisionMaker : MonoBehaviour
    {
        [SerializeField] private List<NpcAction> availableActions;
        
        private NpcController _npcController;

        public IEnumerable<NpcAction> AvailableActions => availableActions;
        private NpcAction _currentAction;
        
        private void Start()
        {
            _npcController = GetComponent<NpcController>();
            CalculateNewDecision<GenericContext>();
        }

        public void CalculateNewDecision<T>() where T : IConsiderationContext
        {
            if (!availableActions.Any(x => x is not null)) return;
            
            var context = GenerateContext(typeof(T));

            // if (_currentAction is not null)
            // {
            //     _currentAction.EventExecuted -= EventExecuted;
            // }

            _currentAction = DecideBestAction(availableActions, context);
            
            //_currentAction.EventExecuted += EventExecuted;
            
            _currentAction.Execute(_npcController);
        }

        // private void EventExecuted(object sender, EventArgs e)
        // {
        //     Debug.Log("Gonna figure out what to do next....");
        //     CalculateNewDecision<GenericContext>();
        // }

        private IConsiderationContext GenerateContext(Type t)
        {
            return t switch 
            {
                not null when t == typeof(SpeechConsiderationContext) => new SpeechConsiderationContext(_npcController.Disposition),
                _ => new GenericContext()
            };
        }

        private static NpcAction DecideBestAction(IEnumerable<NpcAction> actions, IConsiderationContext context)
        {
            var scores = new Dictionary<NpcAction, float>();
            foreach (var action in actions)
            {
                var score = action.CalculateScore(context);
                scores.Add(action,score);
            }
            return scores.OrderByDescending(x => x.Value).First().Key;
        }

        public void ScheduleItem(ScheduleItem scheduleItem)
        {
            availableActions.Add(scheduleItem.Action);
            CalculateNewDecision<GenericContext>();
        }
        
        public void UnScheduleItem(ScheduleItem scheduleItem)
        {
            availableActions.Remove(scheduleItem.Action);
        }
    }
}
