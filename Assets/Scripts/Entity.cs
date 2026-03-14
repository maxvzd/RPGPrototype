using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Guid Id { get; private set; } = Guid.NewGuid();
}