using System;
using Constants;
using PlayerMovement;
using UnityEngine;

public class SheatheManager : MonoBehaviour
{
    public bool IsWeaponSheathed => _isWeaponSheathed;

    [SerializeField] private Transform rightHandSocket;
    [SerializeField] private Transform sheathedSocket;
    [SerializeField] private Transform weaponTransform;
    
    [SerializeField] private Vector3 sheathedPosition;
    [SerializeField] private Vector3 sheathedRotation;
    [SerializeField] private Vector3 wieldedPosition;
    [SerializeField] private Vector3 wieldedRotation;
    
    private Animator _animator;
    private bool _isWeaponSheathed = true;
    private FpArmsAnimationEventHandler _animationEventHandler;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _animationEventHandler = GetComponent<FpArmsAnimationEventHandler>();
        _animationEventHandler.WeaponSheathed += WeaponSheathed;
        _animationEventHandler.WeaponUnSheathed += WeaponSheathed;
        _animator.SetBool(AnimatorConstants.WeaponSheathed, _isWeaponSheathed);
        UpdateWeaponPosition();
    }

    private void WeaponSheathed(object sender, EventArgs e)
    {
        UpdateWeaponPosition();
    }

    public void SheatheWeapon()
    {
        _isWeaponSheathed = !_isWeaponSheathed;
        _animator.SetBool(AnimatorConstants.WeaponSheathed, _isWeaponSheathed);
    }

    private void UpdateWeaponPosition()
    {
        if (!_isWeaponSheathed)
        {
            PlayerTurner.BodyShouldFollowCameraRegister();
            weaponTransform.SetParent(rightHandSocket);
            weaponTransform.SetLocalPositionAndRotation(wieldedPosition, Quaternion.Euler(wieldedRotation));
        }
        else
        {
            PlayerTurner.BodyShouldFollowCameraUnRegister();
            weaponTransform.SetParent(sheathedSocket);
            weaponTransform.SetLocalPositionAndRotation(sheathedPosition, Quaternion.Euler(sheathedRotation));
        }
    }
}
