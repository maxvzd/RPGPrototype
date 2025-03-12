using System;

namespace NPC.UtilityBaseClasses.Contexts
{
    public class SpeechContext : IConsiderationContext
    {
        public float Disposition { get; }
        
        public SpeechContext(float disposition)
        {
            Disposition = disposition;
        }
    }
}