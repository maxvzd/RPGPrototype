using System;
using System.Collections;
using UnityEngine;

namespace NPC.WorkerPrototyping.Actions
{
    [CreateAssetMenu(menuName = "Workers/Actions/Work")]
    [Serializable]
    public class WorkAction : WorkerAction
    {
        public override IEnumerator Execute(Guid id)
        {
            var controller = WorkerEntities.Workers[id].Controller;
            yield return controller.Work();
        }
    }
}