using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NPC.WorkerPrototyping
{
    public class WorkerBehaviour : MonoBehaviour
    {
        [SerializeField] private WorkerGoal[] defaultGoals = Array.Empty<WorkerGoal>();
        
        private WorkerEntity _workerEntity;
        public Guid Id { get; private set; }

        private void Awake()
        {
            var entity = GetComponent<Entity>();
            Id = entity.Id;
            
            var navMeshAgent = GetComponent<NavMeshAgent>();
            
            var workerState = GetComponent<WorkerState>();
            var workerController = new WorkerController(navMeshAgent, workerState);
            var workerBrain = new WorkerBrain(Id, defaultGoals);
            _workerEntity = WorkerEntity.Create(Id, workerState, workerBrain, workerController);
            
            workerBrain.ExecuteCoroutine += ExecuteCoroutine;
            workerBrain.Start();
        }

        private void ExecuteCoroutine(object sender, IEnumerator e)
        {
            StartCoroutine(e);
        }
    }
}