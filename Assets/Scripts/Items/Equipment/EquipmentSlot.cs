using System;
using System.Collections.Generic;
using System.Linq;

namespace Items.Equipment
{
    public interface IEquipmentSlot
    {
        bool AddItem(Guid guid);
        bool RemoveItem(Guid guid);
        bool HasEmptySlots();
        bool Contains(Guid guid);
        void RemoveFirstItem();
    }

    public class NullEquipmentSlot : IEquipmentSlot
    {
        public bool AddItem(Guid guid) => false;
        public bool RemoveItem(Guid guid) => false;
        public bool HasEmptySlots() => false;
        public bool Contains(Guid guid) => false;
        public void RemoveFirstItem() { }
    }
    
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

        public void RemoveFirstItem()
        {
            var first = _items.First();

            _items.Remove(first);
        }
        

    }
}