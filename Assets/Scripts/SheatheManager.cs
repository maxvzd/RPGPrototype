using Constants;
using PlayerMovement;
using UnityEngine;

public class SheatheManager : MonoBehaviour
{
    private Animator _animator;
    private bool _isWeaponSheathed = true;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SheatheWeapon()
    {
        _animator.SetTrigger(_isWeaponSheathed ? AnimatorConstants.UnSheatheTrigger : AnimatorConstants.SheatheTrigger);
        if (_isWeaponSheathed)
        {
            PlayerTurner.BodyShouldFollowCameraRegister();
        }
        else
        {
            PlayerTurner.BodyShouldFollowCameraUnRegister();
        }
        
        _isWeaponSheathed = !_isWeaponSheathed;
    }
}
