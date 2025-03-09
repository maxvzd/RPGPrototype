using System;
using System.Linq;
using UnityEngine;

namespace NPC.UtilityBaseClasses
{
    [Serializable]
    public abstract class NpcAction : ScriptableObject
    {
        public abstract void Execute(NpcController npcController);
        [SerializeField] private Consideration[] considerations;

        public float CalculateScore()
        {
            //TODO: update this
            return considerations.Sum(x => x.Score());
        }
    }
}