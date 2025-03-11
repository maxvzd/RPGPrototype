using System;
using UnityEngine;

public class PlayerAnimationEventListener : MonoBehaviour
{
    public EventHandler FinishedTurning;
    
    private void OnFinishedTurning()
    {
        FinishedTurning?.Invoke(this, EventArgs.Empty);
    }
}