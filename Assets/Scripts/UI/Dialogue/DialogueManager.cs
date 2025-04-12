using System;
using System.Collections.Generic;

namespace UI.Dialogue
{
    public class DialogueManager : BaseUIManager
    {
        public static DialogueManager Instance { get; private set; }
        
        private DialogueController _controller;
        private IEnumerable<string> _dialogueOptions;

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else
            {
                throw new Exception("More than one dialogue manager created");
            }
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