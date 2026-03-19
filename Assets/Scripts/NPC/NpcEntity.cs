using System;
using System.Collections;
using System.Linq;
using NPC.ScriptableObjectContexts;
using NPC.UtilityBaseClasses;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcEntity : Entity
    {
        [SerializeField] private GoalAndContext[] goals = Array.Empty<GoalAndContext>();
        
        private NpcInfo _npcInfo;
        public NpcInfo NpcInfo => _npcInfo; 

        private void Awake()
        {
            var navMeshAgent = GetComponent<NavMeshAgent>();
            
            var state = GetComponent<NpcState>();
            var controller = new NpcController(navMeshAgent, state);

            var goalsAndContexts = goals.Select(x => new UtilityBrain.GoalInfo(x.Goal, x.Context.Get(state))); 
            var brain = new UtilityBrain(Id, goalsAndContexts);
            _npcInfo = NpcInfo.Create(this, state, brain, controller);
            Entities.Register(this); 
            
            brain.ExecuteCoroutine += ExecuteCoroutine;
            brain.Start();
        }

        private void ExecuteCoroutine(object sender, IEnumerator e)
        {
            StartCoroutine(e);
        }

        
        [Serializable]
        private class GoalAndContext
        {
            [SerializeField] private UtilityGoal goal;
            [SerializeField] private ScriptableObjectContext context;
            
            public UtilityGoal Goal => goal;
            public ScriptableObjectContext Context => context;
        }
    }
}