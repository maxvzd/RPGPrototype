using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using Items.InstancePropertiesClasses;
using UnityEngine.UIElements;

namespace UI.Inventory
{
    public class InventoryListController
    {
        private readonly MultiColumnListView _listView;

        public int CurrentlyHoveredIndex { get; private set; } = -1;
        public EventHandler<int> ItemClicked;

        private List<ItemViewModel> _items;

        public InventoryListController(MultiColumnListView listView)
        {
            _listView = listView;
            _listView.selectionType = SelectionType.None;
            _listView.RegisterCallback<MouseDownEvent>(OnItemClicked);
        }
        
        public void ResetCurrentlyHovered()
        {
            CurrentlyHoveredIndex = -1;
        }

        public void SetupInventoryList(IEnumerable<InstanceProperties> model)
        {
            SetItems(model);
            
            _listView.columns["Icon"].bindCell = (element, i) =>
            {
                var iconContainer = element.Q<VisualElement>(InventoryUIConstants.IconElement);
                if (iconContainer is not null)
                {
                    iconContainer.style.backgroundImage = _items[i].InventoryIcon;
                }
                BindToEvents(element, i);
            }; 
            
            _listView.columns["Name"].bindCell = (element, i) =>
            {
                SetTextInDisplayLabel(_items[i].Name, element);
                BindToEvents(element, i);
            }; 
            
            _listView.columns["Weight"].bindCell = (element, i) =>
            {
                SetTextInDisplayLabel(_items[i].Weight.ToString("F"), element);
                BindToEvents(element, i);
            };

            _listView.fixedItemHeight = 40;
        }

        private void BindToEvents(VisualElement element, int index)
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
        
        private void OnItemClicked(MouseDownEvent evt)
        {
            if (evt.button != (int)MouseButton.LeftMouse) return;

            var clickedRow = evt.target as VisualElement;
            
            while (clickedRow != null && !clickedRow.ClassListContains("unity-multi-column-view__row-container"))
            {
                clickedRow = clickedRow.parent;
            }

            if (clickedRow == null) return;

            var rowIndex = clickedRow.parent.IndexOf(clickedRow);
            ItemClicked?.Invoke(this, rowIndex);

            evt.StopPropagation();
        }

        private static void SetTextInDisplayLabel(string text, VisualElement root)
        {
            var label = root.Q<Label>(InventoryUIConstants.DisplayLabel);
            if (label is not null)
            {
                label.text = text;
            }
        }

        public void SetSelectedItems(IEnumerable<int> selectedIndices)
        {
            _listView.SetSelection(selectedIndices);
        }

        public void SetItems(IEnumerable<InstanceProperties> items)
        {
            _items = items.Select(x => new ItemViewModel(x.BaseItemProperties)).ToList();
            _listView.itemsSource = _items;
            _listView.RefreshItems();
        }
    }
}