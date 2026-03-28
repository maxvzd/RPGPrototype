using System;
using System.Collections.Generic;
using Registries;

namespace UI.Dialogue
{
    public class DialogueManager : BaseUIManager
    {
        private DialogueController _controller;
        private IEnumerable<string> _dialogueOptions;

        private void Awake()
        {
            UiRegistry.Register(this);
        }

        private void Start()
        {
            _controller = new DialogueController(uiDocument.rootVisualElement);
            _controller.RequestClose += (sender, args) => HideUI();
            HideUI();
            _dialogueOptions = new List<string>();
        }

        //TODO Get rid of state
        public void PopulateDialogueOptions(string dialogue, IEnumerable<string> options)
        {
            _controller.InitDialogue(dialogue);
            _dialogueOptions = options;
        }

        protected override void PopulateItems()
        {
            _controller.PopulateDialogueOptions(_dialogueOptions);
        }
    }
}