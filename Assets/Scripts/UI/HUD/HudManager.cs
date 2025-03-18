using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HUD
{
    public class HudManager : MonoBehaviour
    {
        [SerializeField] private UIDocument hudUI;
        private HudController _hudController;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void Start()
        {
            _hudController = new HudController(hudUI.rootVisualElement);
            HideInteractIcon();
        }

        public void HideInteractIcon()
        {
            _hudController.HideInteractIcon();
        }

        public void ShowInteractIcon()
        {
            _hudController.ShowInteractIcon();
        }
    }
}