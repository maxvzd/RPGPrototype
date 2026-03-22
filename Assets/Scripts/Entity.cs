using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}