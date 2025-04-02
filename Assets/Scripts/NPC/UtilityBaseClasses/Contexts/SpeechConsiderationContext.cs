using System;
using Interact;

namespace NPC.UtilityBaseClasses.Contexts
{
    public class SpeechConsiderationContext : IConsiderationContext, IInteractionContext
    {
        public float Disposition { get; }
        
        public SpeechConsiderationContext(float disposition)
        {
            Disposition = disposition;
        }
    }
}