using System;
using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}

public interface IEntity
{
    Guid Id { get; }
}