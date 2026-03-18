using System;
using System.Collections;
using NPC.UtilityBaseClasses;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcEntity : Entity
    {
        [SerializeField] private UtilityGoal[] defaultGoals = Array.Empty<UtilityGoal>();
        
        private NpcInfo _npcInfo;
        public NpcInfo NpcInfo => _npcInfo; 

        private void Awake()
        {
            var navMeshAgent = GetComponent<NavMeshAgent>();
            
            var workerState = GetComponent<NpcState>();
            var workerController = new NpcController(navMeshAgent, workerState);
            var workerBrain = new UtilityBrain(Id, defaultGoals);
            _npcInfo = NpcInfo.Create(this, workerState, workerBrain, workerController);
            Entities.Register(this); 
            
            workerBrain.ExecuteCoroutine += ExecuteCoroutine;
            workerBrain.Start();
            
        }

        private void ExecuteCoroutine(object sender, IEnumerator e)
        {
            StartCoroutine(e);
        }
    }
}