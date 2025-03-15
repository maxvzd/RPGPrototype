using System;
using UnityEngine;

public class FpArmsAnimationEventHandler : MonoBehaviour
{
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
}