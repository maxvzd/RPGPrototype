using System;
using System.Collections;
using NPC.Context;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    public class GreetEntity : UtilityAction
    {
        public override IEnumerator Execute(Guid id, NpcContext context)
        {
            Debug.Log("Hello player!");
            yield return new WaitForSeconds(0.5f);
        }
    }
}