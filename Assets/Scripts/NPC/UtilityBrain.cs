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


        public UtilityBrain(Guid id, IEnumerable<GoalInfo> workerGoals)
        {
            Id = id;
            _goals = workerGoals.ToDictionary(x => Guid.NewGuid(), x => new GoalInfo(x.Goal, x.Context));
        }

        private IEnumerator Run()
        {
            while (true)
            {
                //if (!_goals.Any(x => x.Value is not null)) yield break;
                var goal = DecideBestGoal(_goals.Values);
                yield return goal.Goal.ExecuteActions(Id, goal.Context);
            }
        }

        public void Start()
        {
            ExecuteCoroutine?.Invoke(this, Run());
        }
        
        private GoalInfo DecideBestGoal(IEnumerable<GoalInfo> goals)
        {
            return UtilityAiUtilities.Evaluate(goals.ToDictionary(x => x, x=> x.Context), Id);
        }

        public void SpottedEntity(Guid id)
        {
            var goal = NpcUtilityLoader.Instance.Goals["GreetEntity"];
            var reference = UtilityGoal.GenerateReference();
            var spottedEntity = EntitiesRegistry.Dictionary[id];

            var context = new NpcContext();
            context.Add(ContextKeys.Target, spottedEntity.gameObject);
            
            _goals.Add(reference, new GoalInfo(goal, context));
        }

        public class GoalInfo : IEvaluate
        {
            public UtilityGoal Goal { get; }
            public NpcContext Context { get; }
            
            public GoalInfo(UtilityGoal goal, NpcContext context)
            {
                Goal = goal;
                Context = context;
            }

            public float Evaluate(Guid id, NpcContext context)
            {
                return Goal.Evaluate(id, context);
            }
        }
    }
}
