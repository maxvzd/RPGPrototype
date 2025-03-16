using System;
using Interact;

namespace NPC.UtilityBaseClasses.Contexts
{
    public class SpeechContext : IConsiderationContext, IInteractionContext
    {
        public float Disposition { get; }
        
        public SpeechContext(float disposition)
        {
            Disposition = disposition;
        }
    }
}