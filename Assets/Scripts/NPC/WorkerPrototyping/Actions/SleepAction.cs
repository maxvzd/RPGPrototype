using System;
using UnityEngine;

namespace NPC.WorkerPrototyping.Actions
{
    [CreateAssetMenu(menuName = "Workers/Actions/Sleep")]
    [Serializable]
    public class SleepAction: WorkerAction
    {
        private WorkerController _controller;
        
        public override void Execute(Guid id)
        {
            _controller = WorkerEntities.Workers[id].Controller;
            _controller.ActionFinished += OnActionFinished;
            _controller.Sleep();
        }
        
        private void OnActionFinished(object sender, EventArgs e)
        {
            _controller.ActionFinished -= OnActionFinished;
            FireActionFinished();
        }
    }
}