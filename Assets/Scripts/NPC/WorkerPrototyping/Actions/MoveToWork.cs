using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Actions
{
    [CreateAssetMenu(menuName = "Workers/Actions/MoveToWork")]
    [Serializable]
    public class MoveToWork : WorkerAction
    {
        private WorkerController _controller;

        public override void Execute(Guid id)
        {
            var destination = WorkerEntities.Workers[id].State.Work.position;
            Debug.Log("Moving to Work");
            _controller = WorkerEntities.Workers[id].Controller;
            _controller.ActionFinished += OnActionFinished;
            _controller.MoveToGameObject(destination);
        }

        private void OnActionFinished(object sender, EventArgs e)
        {
            _controller.ActionFinished -= OnActionFinished;
            FireActionFinished();
        }
    }
}