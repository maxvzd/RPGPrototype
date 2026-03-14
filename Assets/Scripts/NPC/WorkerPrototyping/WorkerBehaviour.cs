using System;
using UnityEngine;

namespace NPC.WorkerPrototyping
{
    public class WorkerBehaviour : MonoBehaviour
    {
        private WorkerEntity _workerEntity;
        public Guid Id { get; private set; }

        private void Awake()
        {
            Id = Guid.NewGuid();
            
            var workerState = GetComponent<WorkerState>();
            var workerController = GetComponent<WorkerController>();
            var workerBrain = GetComponent<WorkerBrain>();
            
            _workerEntity = WorkerEntity.Create(Id, workerState, workerBrain, workerController);
             WorkerEntities.Register(_workerEntity);
        }
    }
}