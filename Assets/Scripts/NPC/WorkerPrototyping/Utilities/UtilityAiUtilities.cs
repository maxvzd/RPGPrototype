using System;
using System.Collections.Generic;
using System.Linq;

namespace NPC.WorkerPrototyping.Utilities
{
    public static class UtilityAiUtilities
    {
        public static T Evaluate<T>(IEnumerable<T> actions, Guid id) where T : IEvaluate
        {
            var scores = new Dictionary<T, float>();
            foreach (var action in actions)
            {
                var score = action.Evaluate(id);
                scores.TryAdd(action, score);
            }
            return scores.OrderByDescending(x => x.Value).First().Key;
        }
        
        public static float EvaluateConsiderations(Guid id, WorkerConsiderationBase[] considerations)
        {
            if (considerations is null || considerations.Length == 0) return 0;
            
            var score = 1f;
            foreach (var consideration in considerations)
            {
                var considerationScore = consideration.Score(id);
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