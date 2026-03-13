using System;
using System.Collections;
using UnityEngine;

namespace NPC.WorkerPrototyping.Actions
{
    [CreateAssetMenu(menuName = "Workers/Actions/Eat")]
    [Serializable]
    public class EatAction : WorkerAction
    {
        public override IEnumerator Execute(Guid id)
        {
            var controller = WorkerEntities.Workers[id].Controller;
            yield return controller.Eat();
            FireActionFinished();
        }
    }
}