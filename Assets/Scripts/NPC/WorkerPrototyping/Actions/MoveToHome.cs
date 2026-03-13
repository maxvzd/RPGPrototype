using System;
using System.Collections;
using UnityEngine;

namespace NPC.WorkerPrototyping.Actions
{
    [CreateAssetMenu(menuName = "Workers/Actions/MoveToHome")]
    [Serializable]
    public class MoveToHome : WorkerAction
    {

        public override IEnumerator Execute(Guid id)
        {
            var destination = WorkerEntities.Workers[id].State.Home.position;
            var controller = WorkerEntities.Workers[id].Controller;
            yield return controller.MoveToGameObject(destination);
            FireActionFinished();
        }
    }
}