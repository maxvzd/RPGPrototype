using System;
using Items;
using Items.Equipment;
using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public Inventory Inventory { get; private set; }
    public EquipmentSlotManager EquipmentSlots { get; private set; }
    

    protected virtual void Awake()
    {
        Inventory = GetComponent<Inventory>();
        EquipmentSlots = GetComponent<EquipmentSlotManager>();
    }
}

public interface IEntity
{
    Guid Id { get; }
}