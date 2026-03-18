using System;

namespace NPC
{
    public class NpcInfo
    {
        public Guid Id { get; }

        public NpcState State { get; }

        public UtilityBrain Brain { get; }

        public NpcController Controller { get; }
        public Entity Entity { get; }
        
        public static NpcInfo Create(Entity entity, NpcState state, UtilityBrain brain, NpcController controller) => new(entity, state, brain, controller);

        private NpcInfo(Entity entity, NpcState state, UtilityBrain brain, NpcController controller)
        {
            Id = entity.Id;
            State = state;
            Brain = brain;
            Controller = controller;
            Entity = entity;
        }

    }
}