using System;
using Items;
using Registries;
using UnityEngine;

public class Container : MonoBehaviour, IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public Inventory Inventory { get; private set; }

    public void Awake()
    {
        ContainerRegistry.Register(this);
    }

    private void Start()
    {
        Inventory = GetComponent<Inventory>();
    }
}
