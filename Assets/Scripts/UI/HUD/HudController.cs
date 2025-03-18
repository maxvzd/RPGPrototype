using Constants;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HUD
{
    public class HudController
    {
        private readonly VisualElement _interactIcon;

        public HudController(VisualElement root)
        {
            _interactIcon = root.Q(InventoryHudConstants.InteractIcon);
        }

        public void ShowInteractIcon()
        {
            _interactIcon.style.display = DisplayStyle.Flex;
        }

        public void HideInteractIcon()
        {
            _interactIcon.style.display = DisplayStyle.None;
        }

        public void UpdateInteractIcon(Texture2D icon)
        {
            _interactIcon.style.backgroundImage = icon;
        }
    }
}