using System.Collections.Generic;
using Items;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const float WEIGHT_LIMIT = 10;
    private float _currentWeight = 0;

    private List<ItemProperties> _items = new();

    public IEnumerable<ItemProperties> Items => _items;
    
    public bool AddItem(ItemProperties item)
    {
        if (!(_currentWeight + item.Weight <= WEIGHT_LIMIT))
        {
            Debug.Log($"Couldn't add {item.ItemName} as it's too heavy, Current weight: {_currentWeight}");
            return false;
        }

        _currentWeight += item.Weight;
        _items.Add(item);
        Debug.Log($"Added {item.ItemName}, Current weight: {_currentWeight}");
        return true;
    }
    
    public bool RemoveItem(ItemProperties item)
    {
        if (!_items.Exists(x => item)) return false;
        
        _currentWeight -= item.Weight;
        _items.Remove(item);
        Debug.Log($"Removed {item.ItemName}, Current weight: {_currentWeight}");
        return true;
    }
}