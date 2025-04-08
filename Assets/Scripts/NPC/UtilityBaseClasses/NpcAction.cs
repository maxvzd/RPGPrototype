using System;
using UnityEngine;

namespace NPC.UtilityBaseClasses
{
    [Serializable]
    public abstract class NpcAction : ScriptableObject
    {
        [SerializeField] private string actionId;
        [SerializeField] private ConsiderationBase[] considerations;

        //public EventHandler EventExecuted;
        
        public abstract void Execute(NpcController npcController);
        
        public float CalculateScore(IConsiderationContext context)
        {
            if (considerations is null || considerations.Length == 0) return 0;
            
            var score = 1f;
            foreach (var consideration in considerations)
            {
                var considerationScore = consideration.Score(context);
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