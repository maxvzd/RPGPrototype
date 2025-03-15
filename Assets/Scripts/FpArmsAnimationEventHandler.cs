using System;
using UnityEngine;

public class FpArmsAnimationEventHandler : MonoBehaviour
{
    public EventHandler ReadyToAttack;
    public EventHandler SwingFinished;
    
    private void OnReadyToAttack()
    {
        ReadyToAttack?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnSwingFinished()
    {
        SwingFinished?.Invoke(this, EventArgs.Empty);
    }
}