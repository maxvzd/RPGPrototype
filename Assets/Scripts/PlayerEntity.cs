using System;
using NPC;

public class PlayerEntity : Entity
{
    private void Awake()
    {
        EntitiesRegistry.Register(this);
    }
}