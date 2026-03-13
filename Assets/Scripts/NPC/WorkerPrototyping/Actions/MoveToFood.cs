using System;
using System.Collections;
using UnityEngine;

namespace NPC.WorkerPrototyping.Actions
{
    [CreateAssetMenu(menuName = "Workers/Actions/MoveToFood")]
    [Serializable]
    public class MoveToFood : WorkerAction
    {
        public override IEnumerator Execute(Guid id)
        {
            var destination = WorkerEntities.Workers[id].State.Food.position;
            var controller = WorkerEntities.Workers[id].Controller;
            yield return controller.MoveToGameObject(destination);
            
            FireActionFinished();
            
        }
    }
}