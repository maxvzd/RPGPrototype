using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HUD
{
    public class HudManager : MonoBehaviour
    {
        public bool InteractionIconShowing { get; private set; }
        
        [SerializeField] private UIDocument hudUI;
        private HudController _hudController;

        public void Start()
        {
            _hudController = new HudController(hudUI.rootVisualElement);
            HideInteractIcon();
        }

        public void HideInteractIcon()
        {
            _hudController.HideInteractIcon();
            InteractionIconShowing = false;
        }

        public void ShowInteractIcon()
        {
            _hudController.ShowInteractIcon();
            InteractionIconShowing = true;
        }

        public void UpdateInteractionIcon(string iconName)
        {
            var icon = Resources.Load<Texture2D>($"Icons/{iconName}");
            _hudController.UpdateInteractIcon(icon);
        }
    }
}