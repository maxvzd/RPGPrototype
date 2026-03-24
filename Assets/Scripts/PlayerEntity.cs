using Input;
using NPC;
using UnityEngine;

public class PlayerEntity : Entity
{
    public IPlayerInputState Input { get; private set; }
    public CameraLook CameraLook { get; private set; }

    private void Awake()
    {
        Input = GetComponent<PlayerInputHandler>().State;
        CameraLook = GetComponent<CameraLook>();
        EntitiesRegistry.Register(this);
    }

}