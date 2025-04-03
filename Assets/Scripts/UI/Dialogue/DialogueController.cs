using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Dialogue
{
    public class DialogueController
    {
        private readonly ListView _dialogueListView;
        private List<string> _dialogueOptions = new();

        public EventHandler RequestClose;

        public DialogueController(VisualElement root)
        {
            _dialogueListView = root.Q<ListView>(DialogueUiConstants.DialogueListView);
            var closeButton = root.Q<Button>(DialogueUiConstants.CloseButton);
            closeButton.clicked += () =>
            {
                RequestClose?.Invoke(this, EventArgs.Empty);
            };
        }

        public void PopulateDialogueOptions(IEnumerable<string> options)
        {
            Debug.Log("Setting items");
            _dialogueOptions = options.ToList();
            
            _dialogueListView.itemsSource = _dialogueOptions;
            _dialogueListView.bindItem = (item, index) =>
            {
                var text = _dialogueOptions[index];
                var button = item.Q<Button>();
                button.text = text;
            };
            _dialogueListView.RefreshItems();
        }
    }
}