using Interact.Contexts;
using UI.Dialogue;

namespace Interact.ContextBuilders
{
    public class SpeechContextBuilder : IContextBuilder
    {
        public SpeechInteractionContext Build(DialogueManager dialogueManager)
        {
            return new SpeechInteractionContext(dialogueManager);
        }
    }
}