using System;
using UnityEngine;

public class FpArmsAnimationEventHandler : MonoBehaviour
{
    public EventHandler ReadyToAttack;
    
    private void OnReadyToAttack()
    {
        ReadyToAttack?.Invoke(this, EventArgs.Empty);
    }
}