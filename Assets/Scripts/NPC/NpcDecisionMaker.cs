using System;
using System.Collections.Generic;
using System.Linq;
using NPC.Considerations;
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

        private ConsiderationContextGenerator _contextGenerator;
        
        private void Start()
        {
            _npcController = GetComponent<NpcController>();
            
            var socialStats = GetComponent<SocialStats>();

            _contextGenerator = new ConsiderationContextGenerator(socialStats);
            
            CalculateNewDecision();
        }

        public void CalculateNewDecision()
        {
            if (!availableActions.Any(x => x is not null)) return;
            

            // if (_currentAction is not null)
            // {
            //     _currentAction.EventExecuted -= EventExecuted;
            // }

            _currentAction = DecideBestAction();
            
            //_currentAction.EventExecuted += EventExecuted;
            
            _currentAction.Execute(_npcController);
        }

        // private void EventExecuted(object sender, EventArgs e)
        // {
        //     Debug.Log("Gonna figure out what to do next....");
        //     CalculateNewDecision<GenericContext>();
        // }

        private NpcAction DecideBestAction()
        {
            var scores = new Dictionary<NpcAction, float>();
            var instantActions = new List<NpcAction>();
            foreach (var action in availableActions)
            {
                var score = action.CalculateScore(_contextGenerator);
                scores.TryAdd(action, score);
                
                if (action is InstantAction)
                {
                    instantActions.Add(action);
                }
            }

            foreach (var instantAction in instantActions)
            {
                availableActions.Remove(instantAction);
            }
            return scores.OrderByDescending(x => x.Value).First().Key;
        }

        public void AddSpeechAction(IEnumerable<NpcAction> actions)
        {
            availableActions.AddRange(actions);
        }

        public void ScheduleItem(ScheduleItem scheduleItem)
        {
            availableActions.Add(scheduleItem.Action);
            CalculateNewDecision();
        }
        
        public void UnScheduleItem(ScheduleItem scheduleItem)
        {
            availableActions.Remove(scheduleItem.Action);
        }
    }
}
