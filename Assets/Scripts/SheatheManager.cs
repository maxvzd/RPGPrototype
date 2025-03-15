using Constants;
using PlayerMovement;
using UnityEngine;

public class SheatheManager : MonoBehaviour
{
    public bool IsWeaponSheathed => _isWeaponSheathed;
    
    private Animator _animator;
    private bool _isWeaponSheathed = true;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(AnimatorConstants.WeaponSheathed, _isWeaponSheathed);
    }

    public void SheatheWeapon()
    {
        _isWeaponSheathed = !_isWeaponSheathed;

        _animator.SetBool(AnimatorConstants.WeaponSheathed, _isWeaponSheathed);
        if (!_isWeaponSheathed)
        {
            PlayerTurner.BodyShouldFollowCameraRegister();
        }
        else
        {
            PlayerTurner.BodyShouldFollowCameraUnRegister();
        }
    }
}
