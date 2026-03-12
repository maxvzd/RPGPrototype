using System;
using System.Collections.Generic;

namespace NPC.WorkerPrototyping
{
    public static class WorkerEntities
    {
        private static Dictionary<Guid, WorkerEntity> _workers =  new();
        public static IReadOnlyDictionary<Guid, WorkerEntity> Workers => _workers;
        
        public static void Register(WorkerEntity worker)
        {
            _workers.Add(worker.Id, worker);
        }
    }
}