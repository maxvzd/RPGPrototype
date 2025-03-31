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
        [SerializeField] private List<NpcAction> speechActions;
        
        private NpcController _npcController;

        public IEnumerable<NpcAction> AvailableActions => availableActions;

        private void Start()
        {
            _npcController = GetComponent<NpcController>();
        }

        private void Update()
        {
            //if (_npcController.IsIdle)
            //{
                //CalculateDecision(availableActions, _npcController, new GenericContext());
            //}
        }

        public void DecideSpeechInteractionWithPlayer()
        {
            CalculateDecision(speechActions, _npcController, new SpeechContext(_npcController.Disposition));
        }
        
        public static void CalculateDecision(IEnumerable<NpcAction> actions, NpcController controller, IConsiderationContext context)
        {
            var scores = new Dictionary<NpcAction, float>();
            foreach (var action in actions)
            {
                var score = action.CalculateScore(context);
                scores.Add(action,score);
            }
            scores.OrderByDescending(x => x.Value).First().Key.Execute(controller);
        }
    }
}
