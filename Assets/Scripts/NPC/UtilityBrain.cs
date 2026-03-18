using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NPC.Context;
using NPC.UtilityBaseClasses;

namespace NPC
{
    public class UtilityBrain
    {
        public EventHandler<IEnumerator> ExecuteCoroutine;
        
        private readonly Dictionary<Guid, GoalInfo> _goals;
        
        private Guid Id { get; }


        public UtilityBrain(Guid id, IEnumerable<UtilityGoal> workerGoals)
        {
            Id = id;
            _goals = workerGoals.ToDictionary(x => Guid.NewGuid(), x => new GoalInfo(x, new NpcContext()));
        }

        private IEnumerator Run()
        {
            while (true)
            {
                //if (!_goals.Any(x => x.Value is not null)) yield break;
                var goal = DecideBestGoal(_goals.Select(x => x.Value.Goal));
                yield return goal.ExecuteActions(Id);
            }
        }

        public void Start()
        {
            ExecuteCoroutine?.Invoke(this, Run());
        }
        
        private UtilityGoal DecideBestGoal(IEnumerable<UtilityGoal> actions)
        {
            return UtilityAiUtilities.Evaluate(actions, Id);
        }

        public void SpottedEntity(Guid id)
        {
            var goal = NpcUtilityLoader.Instance.Goals["GreetEntity"];
            var reference = UtilityGoal.GenerateReference();
            var spottedEntity = Entities.List[id];

            var context = new NpcContext();
            context.Add(ContextKeys.Target, spottedEntity.gameObject);
            
            _goals.Add(reference, new GoalInfo(goal, context));
        }

        private struct GoalInfo
        {
            
            public UtilityGoal Goal { get; }
            public NpcContext NpcContext { get; }
            
            public GoalInfo(UtilityGoal goal, NpcContext npcContext)
            {
                Goal = goal;
                NpcContext = npcContext;
            }
        }
    }
}
