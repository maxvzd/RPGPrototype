using System.Collections.Generic;
using System.Linq;
using Constants;
using Items;
using UnityEngine.UIElements;

namespace UI.Inventory
{
    public class InventoryListController
    {
        private MultiColumnListView _listView;
        
        public InventoryListController(MultiColumnListView listView)
        {
            _listView = listView;
        }

        public void PopulateInventoryList(IEnumerable<ItemProperties> model)
        {
            var listOfViewModels = model.Select(x => new ItemViewModel(x)).ToList();
            
            _listView.itemsSource = listOfViewModels;
            _listView.columns["Icon"].bindCell = (element, i) =>
            {
                var iconContainer = element.Q<VisualElement>(InventoryUIConstants.IconElement);
                if (iconContainer is not null)
                {
                    iconContainer.style.backgroundImage = listOfViewModels[i].InventoryIcon;
                }
            }; 
            _listView.columns["Name"].bindCell = (element, i) => SetTextInDisplayLabel(listOfViewModels[i].Name, element); 
            _listView.columns["Weight"].bindCell = (element, i) => SetTextInDisplayLabel(listOfViewModels[i].Weight.ToString("F"), element); 
            
            _listView.fixedItemHeight = 95;
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