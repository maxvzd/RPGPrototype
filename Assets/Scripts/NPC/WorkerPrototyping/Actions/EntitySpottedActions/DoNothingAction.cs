using System;
using System.Collections;

namespace NPC.WorkerPrototyping.Actions.EntitySpottedActions
{
    public class DoNothingAction : WorkerAction
    {
        public override IEnumerator Execute(Guid id)
        {
            //Do nothing
            yield break;
        }
    }
}