using System;
using UnityEngine;

public class PlayerAnimationEventListener : MonoBehaviour
{
    public EventHandler FinishedTurning;
    public EventHandler ReadyToAttack;
    public EventHandler SwingFinished;
    public EventHandler WeaponSheathed;
    public EventHandler WeaponUnSheathed;

    private void OnReadyToAttack()
    {
        ReadyToAttack?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnSwingFinished()
    {
        SwingFinished?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnWeaponSheathed()
    {
        WeaponSheathed?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnWeaponUnSheathed()
    {
        WeaponUnSheathed?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnFinishedTurning()
    {
        FinishedTurning?.Invoke(this, EventArgs.Empty);
    }
}