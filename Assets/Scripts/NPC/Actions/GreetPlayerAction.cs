using System;
using System.Collections;
using NPC.UtilityBaseClasses;
using UnityEngine;

namespace NPC.Actions
{
    public class GreetEntity : UtilityAction
    {
        public override IEnumerator Execute(Guid id)
        {
            Debug.Log("Hello player!");
            yield return new WaitForSeconds(0.5f);
        }
    }
}