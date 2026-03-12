using System;
using UnityEngine;

namespace NPC.WorkerPrototyping
{
    [Serializable]
    public abstract class WorkerAction : ScriptableObject//, IWorkerAction
    {
        [SerializeField] private WorkerConsiderationBase[] considerations;
        public event EventHandler ActionFinished;
        
        public abstract void Execute(Guid id);
        
        public float CalculateScore(Guid id)
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
        
        protected void FireActionFinished()
        {
            ActionFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}