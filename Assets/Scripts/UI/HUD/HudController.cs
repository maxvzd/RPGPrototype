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
            //_interactIcon = root.Q(InventoryHudConstants.InteractIconContainer);
            _interactIcon = root.Q(InventoryHudConstants.InteractIcon);
            if (_interactIcon is null)
            {
                Debug.Log("Didn't find interacticon");
            }
        }

        public void ShowInteractIcon()
        {
            _interactIcon.style.display = DisplayStyle.Flex;
        }

        public void HideInteractIcon()
        {
            _interactIcon.style.display = DisplayStyle.None;
        }
    }
}