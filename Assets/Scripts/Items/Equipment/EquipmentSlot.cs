using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;

namespace Items.Equipment
{
    public interface IEquipmentSlot
    {
        bool AddItem(Guid guid) => false;
        bool RemoveItem(Guid guid) => false;
        bool HasEmptySlots() => false;
        bool Contains(Guid guid) => false;
        Guid GetFirstItem() => Guid.Empty;
    }

    public class EmptyEquipmentSlot : IEquipmentSlot { }
    
    public class EquipmentSlot : IEquipmentSlot
    {
        private readonly HashSet<Guid> _items = new();
        private readonly int _maxNumberOfItemsInSlot;

        public EquipmentSlot(int maxNumberOfItemsInSlot)
        {
            _maxNumberOfItemsInSlot = maxNumberOfItemsInSlot;
        }

        public bool AddItem(Guid guid) => _items.Count + 1 <= _maxNumberOfItemsInSlot && _items.Add(guid);
        public bool RemoveItem(Guid guid) => _items.Remove(guid);
        public bool Contains(Guid guid) => _items.Contains(guid);
        public bool HasEmptySlots() => _items.Count < _maxNumberOfItemsInSlot;
        public Guid GetFirstItem() => _items.FirstOrDefault();
    }
}