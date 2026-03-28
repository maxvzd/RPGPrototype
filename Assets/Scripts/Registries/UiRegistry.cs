using UI.Container;
using UI.Dialogue;
using UI.Inventory;

namespace Registries
{
    public static class UiRegistry
    {
        private static PlayerInventoryUiManager _inventoryUi;
        private static DialogueManager _dialogueUi;
        private static ContainerUiManager _containerUi;
        
        public static ContainerUiManager ContainerUi => _containerUi;
        public static DialogueManager DialogueUi => _dialogueUi;
        public static PlayerInventoryUiManager InventoryUi => _inventoryUi;

        public static void Register(ContainerUiManager ui)
        {
            _containerUi = ui;
        }
        
        public static void Register(DialogueManager ui)
        {
            _dialogueUi = ui;
        }
        
        public static void Register(PlayerInventoryUiManager ui)
        {
            _inventoryUi = ui;
        }
    }
}