using Combat.LockOn;
using Input;
using Items;
using NPC;

public class PlayerEntity : Entity
{
    public IPlayerInputState Input { get; private set; }
    public CameraLook CameraLook { get; private set; }
    public LockOn LockOn { get; private set; }
    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        Input = GetComponent<PlayerInputHandler>().State;
        CameraLook = GetComponent<CameraLook>();
        LockOn = GetComponent<LockOn>();
        Inventory = GetComponent<Inventory>();
        
        EntitiesRegistry.Register(this);
    }
}