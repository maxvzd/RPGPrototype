using System;
using UnityEngine;

namespace NPC.WorkerPrototyping
{
    public class WorkerBehaviour : MonoBehaviour
    {
        private WorkerEntity _workerEntity;

        private void Start()
        {
            var id = Guid.NewGuid();
            
            var workerState = GetComponent<WorkerState>();
            var workerController = GetComponent<WorkerController>();
            var workerBrain = GetComponent<WorkerBrain>();
            workerBrain.Init(id);
            
            _workerEntity = WorkerEntity.Create(id, workerState, workerBrain, workerController);
             WorkerEntities.Register(_workerEntity);
            _workerEntity.Brain.CalculateNewDecision();
        }
    }
}