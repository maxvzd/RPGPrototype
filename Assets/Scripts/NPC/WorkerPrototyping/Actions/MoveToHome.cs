using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Actions
{
    [CreateAssetMenu(menuName = "Workers/Actions/MoveToHome")]
    [Serializable]
    public class MoveToHome : WorkerAction
    {
        private WorkerController _controller;

        public override void Execute(Guid id)
        {
            var destination = WorkerEntities.Workers[id].State.Home.position;
            
            Debug.Log("Moving to home");
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