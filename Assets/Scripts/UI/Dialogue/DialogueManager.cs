using System.Collections.Generic;

namespace UI.Dialogue
{
    public class DialogueManager : BaseUIManager
    {
        private DialogueController _controller;
        private IEnumerable<string> _dialogueOptions;

        private void Start()
        {
            _controller = new DialogueController(uiDocument.rootVisualElement);
            _controller.RequestClose += (sender, args) => HideUI();
            HideUI();
            _dialogueOptions = new List<string>();
        }

        //TODO Get rid of state
        public void PopulateDialogueOptions(IEnumerable<string> options)
        {
            _dialogueOptions = options;
        }

        protected override void PopulateItems()
        {
            _controller.PopulateDialogueOptions(_dialogueOptions);
        }
    }
}