﻿using NPC.UtilityBaseClasses;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPC.Actions
{
    [CreateAssetMenu(menuName = "NPC/Actions/Walk")]
    public class WalkAction : NpcAction
    {
        public override void Execute(NpcController npcController)
        {
            var pos = Random.insideUnitSphere * 10f;
            pos.y = 0f;
            
            npcController.MoveToDestination(pos);
        }
    }
}