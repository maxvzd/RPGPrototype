using System;
using Constants;
using UnityEngine;

public class FirstPersonCameraSwap : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera fpCamera;

    private bool _weaponIsSheathed = true;

    private readonly string[] _mainCameraWithNoArms = { LayerConstants.Default, LayerConstants.TransparentFx, LayerConstants.IgnoreRaycast, LayerConstants.Water, LayerConstants.Ui, LayerConstants.Terrain };
    private readonly string[] _mainCameraWithArms = { LayerConstants.Default, LayerConstants.TransparentFx, LayerConstants.IgnoreRaycast, LayerConstants.Water, LayerConstants.Ui, LayerConstants.Terrain, LayerConstants.PlayerMeshRightArm, LayerConstants.PlayerMeshLeftArm };
    private readonly string[] _fpCameraWithNoArms = Array.Empty<string>();
    private readonly string[] _fpCameraWithArms = { LayerConstants.FirstPersonLeftArmMesh, LayerConstants.FirstPersonRightArmMesh };
    private FpArmsAnimationEventHandler _animationEventHandler;

    public void Start()
    {
        _animationEventHandler = GetComponent<FpArmsAnimationEventHandler>();
        _animationEventHandler.WeaponSheathed += WeaponSheathed;
    }

    private void WeaponSheathed(object sender, EventArgs e)
    {
        mainCamera.cullingMask = LayerMask.GetMask(_mainCameraWithArms);
        fpCamera.cullingMask = LayerMask.GetMask(_fpCameraWithNoArms);
        _weaponIsSheathed = true;
    }

    public void SwitchArms()
    {
        if (_weaponIsSheathed)
        {
            mainCamera.cullingMask = LayerMask.GetMask(_mainCameraWithNoArms);
            fpCamera.cullingMask = LayerMask.GetMask(_fpCameraWithArms);
            _weaponIsSheathed = false;
        }
    }
}
