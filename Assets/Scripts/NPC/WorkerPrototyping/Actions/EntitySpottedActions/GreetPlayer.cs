using System;
using System.Collections;
using UnityEngine;

namespace NPC.WorkerPrototyping.Actions.EntitySpottedActions
{
    public class GreetPlayer : WorkerAction
    {
        public override IEnumerator Execute(Guid id)
        {
            Debug.Log("Hello player!");
            yield break;
        }
    }
}