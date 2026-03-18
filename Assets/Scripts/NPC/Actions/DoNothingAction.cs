using System;
using System.Collections;
using NPC.UtilityBaseClasses;

namespace NPC.Actions.EntitySpottedActions
{
    public class DoNothingAction : UtilityAction
    {
        public override IEnumerator Execute(Guid id)
        {
            //Do nothing
            yield break;
        }
    }
}