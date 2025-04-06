using System.Collections.Generic;
using Items;
using Items.InstancePropertiesClasses;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private List<ItemInstanceProperties> items;

    private void Start()
    {
        var invent = GetComponent<Inventory>();
        foreach (var item in items)
        {
            invent.AddItem(item);
        }
    }
}
