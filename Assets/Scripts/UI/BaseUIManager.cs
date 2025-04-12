using System;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace UI
{
    public abstract class BaseUIManager : MonoBehaviour 
    {
        [SerializeField] protected UIDocument uiDocument;
        
        public EventHandler UiShown;
        public EventHandler UiHidden;
        
        private bool _uiIsHidden;
        
        public void HideUI()
        {
            if (_uiIsHidden) return;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            _uiIsHidden = true;
            UiHidden?.Invoke(this, EventArgs.Empty);
        }
        
        public virtual void ShowUI()
        {
            if (!_uiIsHidden) return;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            PopulateItems();

            uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            _uiIsHidden = false;
            UiShown?.Invoke(this, EventArgs.Empty);
        }

        protected abstract void PopulateItems();
    }
}