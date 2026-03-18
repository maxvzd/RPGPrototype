using System;
using NPC;

public class PlayerEntity : Entity
{
    private void Awake()
    {
        Entities.Register(this);
    }
}