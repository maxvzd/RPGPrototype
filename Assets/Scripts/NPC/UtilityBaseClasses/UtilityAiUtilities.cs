using System;
using System.Collections.Generic;
using System.Linq;
using NPC.Context;

namespace NPC.UtilityBaseClasses
{
    public static class UtilityAiUtilities
    {
        public static T Evaluate<T>(Dictionary<T, NpcContext> actions, Guid id) where T : IEvaluate
        {
            var scores = new Dictionary<T, float>();
            foreach (var item in actions)
            {
                var evaluatee = item.Key;
                var context = item.Value;
                var score = evaluatee.Evaluate(id, context);
                scores.TryAdd(evaluatee, score);
            }
            return scores.OrderByDescending(x => x.Value).First().Key;
        }
        
        public static float EvaluateConsiderations(Guid id, UtilityConsiderationBase[] considerations, NpcContext context)
        {
            if (considerations is null || considerations.Length == 0) return 0;
            
            var score = 1f;
            foreach (var consideration in considerations)
            {
                var considerationScore = consideration.Score(id, context);
                score *= considerationScore;

                if (score == 0) return 0;
            }

            // Averaging scheme of overall score
            var originalScore = score;
            var modFactor = 1f - 1f / considerations.Length;
            var makeupValue = (1 - originalScore) * modFactor;
            score = originalScore + (makeupValue * originalScore);
            return score;
        }
    }
}