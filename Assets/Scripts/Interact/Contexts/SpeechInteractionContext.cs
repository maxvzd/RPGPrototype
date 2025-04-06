using System.Collections.Generic;
using UI.Dialogue;

namespace Interact.Contexts
{
    public class SpeechInteractionContext : IInteractionContext
    {
        private DialogueManager _dialogueManager;

        public SpeechInteractionContext(DialogueManager dialogueManager)
        {
            _dialogueManager = dialogueManager;
        }

        public void StartDialogue()
        {
            var options = new List<string>();
            for (var i = 0; i < 100; i++)
            {
                options.Add($"This is option: {i}");
            }
            
            _dialogueManager.PopulateDialogueOptions(options);
            _dialogueManager.ShowUI();
        }
    }
}