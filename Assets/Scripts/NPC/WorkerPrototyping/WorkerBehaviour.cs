using System;
using UnityEngine;

namespace NPC.WorkerPrototyping
{
    public class WorkerBehaviour : MonoBehaviour
    {
        [SerializeField] private WorkerAction[] availableActions = Array.Empty<WorkerAction>();
        
        private WorkerEntity _workerEntity;

        private void Start()
        {
            var id = Guid.NewGuid();
            
            var workerState = GetComponent<WorkerState>();
            var workerController = GetComponent<WorkerController>();
            
            _workerEntity = WorkerEntity.Create(id, workerState, new WorkerBrain(id, availableActions), workerController);
             WorkerEntities.Register(_workerEntity);
            _workerEntity.Brain.CalculateNewDecision();
        }
    }
}