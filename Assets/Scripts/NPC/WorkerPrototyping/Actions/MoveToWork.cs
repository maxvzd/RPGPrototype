using System;
using System.Collections;
using UnityEngine;

namespace NPC.WorkerPrototyping.Actions
{
    [CreateAssetMenu(menuName = "Workers/Actions/MoveToWork")]
    [Serializable]
    public class MoveToWork : WorkerAction
    {
        public override IEnumerator Execute(Guid id)
        {
            var destination = WorkerEntities.Workers[id].State.Work.position;
            var controller = WorkerEntities.Workers[id].Controller;
            yield return controller.MoveToGameObject(destination);
        }
    }
}