using System.Collections.Generic;
using System.Linq;
using Constants;
using Items;
using UnityEngine.UIElements;

namespace UI.Inventory
{
    public class InventoryListController
    {
        private readonly MultiColumnListView _listView;

        public int CurrentlyHoveredIndex { get; private set; } = -1;

        public InventoryListController(MultiColumnListView listView)
        {
            _listView = listView;
        }
        
        
        public void ResetCurrentlyHovered()
        {
            CurrentlyHoveredIndex = -1;
        }

        public void PopulateInventoryList(IEnumerable<ItemInstanceProperties> model)
        {
            var listOfViewModels = model.Select(x => new ItemViewModel(x.Item)).ToList();
            
            _listView.itemsSource = listOfViewModels;
            _listView.columns["Icon"].bindCell = (element, i) =>
            {
                var iconContainer = element.Q<VisualElement>(InventoryUIConstants.IconElement);
                if (iconContainer is not null)
                {
                    iconContainer.style.backgroundImage = listOfViewModels[i].InventoryIcon;
                }
            }; 
            _listView.columns["Name"].bindCell = (element, i) =>
            {
                SetTextInDisplayLabel(listOfViewModels[i].Name, element);
                BindToHoverEvents(element, i);
            }; 
            _listView.columns["Weight"].bindCell = (element, i) => SetTextInDisplayLabel(listOfViewModels[i].Weight.ToString("F"), element);

            _listView.fixedItemHeight = 40;
        }

        private void BindToHoverEvents(VisualElement element, int index)
        {
            element.RegisterCallback<PointerEnterEvent>(evt =>
            {
                CurrentlyHoveredIndex = index;
            });
            
            element.RegisterCallback<PointerLeaveEvent>(evt =>
            {
                ResetCurrentlyHovered();
            });
        }

        private static void SetTextInDisplayLabel(string text, VisualElement root)
        {
            var label = root.Q<Label>(InventoryUIConstants.DisplayLabel);
            if (label is not null)
            {
                label.text = text;
            }
        }
    }
}