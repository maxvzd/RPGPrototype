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
            var entity = GetComponent<Entity>();
            Id = entity.Id;
            
            var workerState = GetComponent<WorkerState>();
            var workerController = GetComponent<WorkerController>();
            var workerBrain = GetComponent<WorkerBrain>();
            
            _workerEntity = WorkerEntity.Create(Id, workerState, workerBrain, workerController);
        }
    }
}