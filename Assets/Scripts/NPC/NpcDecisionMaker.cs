using System;
using System.Collections.Generic;
using System.Linq;
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
            _npcController.ReachedDestination += ActionExecuted;
            //CalculateNewDecision<GenericContext>();
        }

        public void CalculateNewDecision<T>() where T : IConsiderationContext
        {
            if (!availableActions.Any()) return;
            
            var context = GenerateContext(typeof(T));
            
            _currentAction = DecideBestAction(availableActions, context);
            
            _currentAction.Execute(_npcController);
        }

        private IConsiderationContext GenerateContext(Type t)
        {
            return t switch 
            {
                not null when t == typeof(SpeechConsiderationContext) => new SpeechConsiderationContext(_npcController.Disposition),
                _ => new GenericContext()
            };
        }

        private void ActionExecuted(object sender, EventArgs e)
        {
            //_npcController.StopFollowingSchedule();
            //CalculateNewDecision<GenericContext>();
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
    }
}
