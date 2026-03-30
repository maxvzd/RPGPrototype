using Combat.LockOn;
using Input;
using NPC;

public class PlayerEntity : Entity
{
    public IPlayerInputState Input { get; private set; }
    public CameraLook CameraLook { get; private set; }
    public LockOn LockOn { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Input = GetComponent<PlayerInputHandler>().State;
        CameraLook = GetComponent<CameraLook>();
        LockOn = GetComponent<LockOn>();
        
        EntitiesRegistry.Register(this);
    }
}