using System;
using System.Collections.Generic;
using Items;
using Items.ItemScriptableObjects;
using Registries;
using UnityEngine;

public class Container : MonoBehaviour, IEntity
{
    [SerializeField] private List<ItemInstanceScriptableObject> items;

    public Guid Id { get; } = Guid.NewGuid();
    public Inventory Inventory { get; private set; }

    public void Awake()
    {
        ContainerRegistry.Register(this);
    }

    private void Start()
    {
        Inventory = GetComponent<Inventory>();
        foreach (var item in items)
        {
            Inventory.AddItem(item.BaseInstance);
        }
    }
}
