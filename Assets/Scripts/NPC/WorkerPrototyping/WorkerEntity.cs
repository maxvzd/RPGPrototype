using System;

namespace NPC.WorkerPrototyping
{
    public class WorkerEntity
    {
        public Guid Id { get; }

        public WorkerState State { get; }

        public WorkerBrain Brain { get; }

        public WorkerController Controller { get; }
        
        public static WorkerEntity Create(Guid id, WorkerState state, WorkerBrain brain, WorkerController controller) => new(id, state, brain, controller);

        private WorkerEntity(Guid id, WorkerState state, WorkerBrain brain, WorkerController controller)
        {
            Id = id;
            State = state;
            Brain = brain;
            Controller = controller;
        }

    }
}