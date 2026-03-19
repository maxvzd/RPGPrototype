using System;
using System.Collections;
using NPC.Context;
using NPC.UtilityBaseClasses;

namespace NPC.Actions.EntitySpottedActions
{
    public class DoNothingAction : UtilityAction
    {
        public override IEnumerator Execute(Guid id, NpcContext context)
        {
            //Do nothing
            yield break;
        }
    }
}