using Interact.Contexts;

namespace Interact.ContextBuilders
{
    public class SpeechInteractionContextBuilder : IContextBuilder
    {
        public SpeechInteractionContext Build()
        {
            return new SpeechInteractionContext();
        }
    }
}