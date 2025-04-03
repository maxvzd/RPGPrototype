using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace UI.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        private bool _uiIsHidden;
        [SerializeField] private UIDocument dialogueUi;
        private DialogueController _controller;

        public EventHandler UiShown;
        public EventHandler UiHidden;

        private void Start()
        {
            _controller = new DialogueController(dialogueUi.rootVisualElement);
            _controller.RequestClose += (sender, args) => HideUI();
            HideUI();
        }

        public void HideUI()
        {
            if (_uiIsHidden) return;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            dialogueUi.rootVisualElement.style.display = DisplayStyle.None;
            _uiIsHidden = true;
            UiHidden?.Invoke(this, EventArgs.Empty);
        }

        public void ShowUI(IEnumerable<string> options)
        {
            if (!_uiIsHidden) return;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            
            _controller.PopulateDialogueOptions(options);

            dialogueUi.rootVisualElement.style.display = DisplayStyle.Flex;
            _uiIsHidden = false;
            UiShown?.Invoke(this, EventArgs.Empty);
        }
    }
}