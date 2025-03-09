using Constants;
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
        _isWeaponSheathed = !_isWeaponSheathed;
    }
}
