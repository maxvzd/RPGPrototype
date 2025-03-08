using System;
using Constants;
using UnityEngine;

public class FirstPersonCameraSwap : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera fpCamera;

    private bool _showingArmsInFp = false;

    private readonly string[] _mainCameraWithNoArms = { LayerConstants.Default, LayerConstants.TransparentFx, LayerConstants.IgnoreRaycast, LayerConstants.Water, LayerConstants.Ui, LayerConstants.Terrain };
    private readonly string[] _mainCameraWithArms = { LayerConstants.Default, LayerConstants.TransparentFx, LayerConstants.IgnoreRaycast, LayerConstants.Water, LayerConstants.Ui, LayerConstants.Terrain, LayerConstants.PlayerMeshRightArm, LayerConstants.PlayerMeshLeftArm };
    private readonly string[] _fpCameraWithNoArms = Array.Empty<string>();
    private readonly string[] _fpCameraWithArms = { LayerConstants.FirstPersonLeftArmMesh };

    public void SwitchArms()
    {
        if (_showingArmsInFp)
        {
            mainCamera.cullingMask = LayerMask.GetMask(_mainCameraWithArms);
            fpCamera.cullingMask = LayerMask.GetMask(_fpCameraWithNoArms);
        }
        else
        {
            mainCamera.cullingMask = LayerMask.GetMask(_mainCameraWithNoArms);
            fpCamera.cullingMask = LayerMask.GetMask(_fpCameraWithArms);
        }

        _showingArmsInFp = !_showingArmsInFp;
    }
}
