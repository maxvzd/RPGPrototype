using System;
using UnityEngine;

public class PlayerAnimationEventListener : MonoBehaviour
{
    public EventHandler ReadyToAttack;
    public EventHandler SwingFinished;
    public EventHandler WeaponSheathed;
    public EventHandler WeaponUnSheathed;
    public EventHandler WeaponDropped;
    public EventHandler OffHandDropped;

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
    
    private void DropItemRight()
    {
        WeaponDropped?.Invoke(this, EventArgs.Empty);
    }
    
    private void DropItemLeft()
    {
        OffHandDropped?.Invoke(this, EventArgs.Empty);
    }
}