using NPC.UtilityBaseClasses.Contexts;

namespace NPC.Considerations
{
    public class ConsiderationContextGenerator
    {
        private readonly SocialStats _socialStats;

        public ConsiderationContextGenerator(SocialStats socialStats)
        {
            _socialStats = socialStats;
        }
        
        public SpeechConsiderationContext GetSpeechContext()
        {
            return new SpeechConsiderationContext(_socialStats.Disposition);
        }
    }
}