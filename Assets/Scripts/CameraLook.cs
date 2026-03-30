using System;
using System.Collections.Generic;
using Constants;
using PlayerMovement;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float maxLookAngleUp;
    [SerializeField] private float maxLookAngleDown;
    [SerializeField] private float maxHorizontalLook;
    [SerializeField] private float maxHorizontalCameraTilt;
    [SerializeField] private float turnTime;

    private ActorMovement _movement;
    private Animator _animator;
    
    private float _targetTilt;
    private float _tiltVelocity;
    private float _zAxisTilt;
    private float _playerYawVelocity;
    public Camera MainCamera => mainCamera;

    private readonly HashSet<Guid> _lockCamera = new();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _movement = GetComponent<ActorMovement>();
        _animator = GetComponent<Animator>();
    }

    public void TiltCamera(float horizontalDirection)
    {
        _targetTilt = horizontalDirection * maxHorizontalCameraTilt * -1;

        var cameraEuler = mainCamera.transform.eulerAngles;
        _zAxisTilt = Mathf.SmoothDampAngle(cameraEuler.z, _targetTilt, ref _tiltVelocity, 0.2f);
    }

    public Guid RegisterCameraLock()
    {
        var token = Guid.NewGuid();
        _lockCamera.Add(token);
        return token;
    }

    public void UnRegisterCameraLock(Guid token)
    {
        _lockCamera.Remove(token);
    }
    
    private void SetTurnRight(bool value)
    {
        _animator.SetBool(AnimatorConstants.ShouldTurnRight, value);
    }
        
    private void SetTurnLeft(bool value)
    {
        _animator.SetBool(AnimatorConstants.ShouldTurnLeft, value);
    }

    public void MoveCamera(Vector2 mouseInput)
    {
        if (_lockCamera.Count > 0)
        {
            SetTurnLeft(false);
            SetTurnRight(false);
            mouseInput.x *= 0.1f;
            mouseInput.y *= 0.1f;
        }

        var currentCameraRotation = cameraContainer.transform.eulerAngles;
        var currentPlayerRotation = transform.eulerAngles;

        var targetCameraPitch = currentCameraRotation.x - mouseInput.y * lookSensitivity;
        var actualCameraPitch = CalculateAndClampAngle(currentPlayerRotation.x, targetCameraPitch, -maxLookAngleUp, maxLookAngleDown);

        var targetCameraYaw = currentCameraRotation.y + mouseInput.x * lookSensitivity;
        var actualCameraYaw = CalculateAndClampAngle(currentPlayerRotation.y, targetCameraYaw, -maxHorizontalLook, maxHorizontalLook);

        var actualPlayerYaw = Mathf.SmoothDampAngle(
            currentPlayerRotation.y,
            actualCameraYaw,
            ref _playerYawVelocity,
            turnTime);
        currentPlayerRotation.y = actualPlayerYaw;
        transform.rotation = Quaternion.Euler(currentPlayerRotation);

        currentCameraRotation.x = actualCameraPitch;
        currentCameraRotation.y = actualCameraYaw;
        currentCameraRotation.z = _zAxisTilt;
        cameraContainer.transform.rotation = Quaternion.Euler(currentCameraRotation);
        
        SetTurnLeft(!_movement.IsMovementKeysDown && mouseInput.x < 0);
        SetTurnRight(!_movement.IsMovementKeysDown && mouseInput.x > 0);
    }
    
    private static float CalculateAndClampAngle(float currentPlayerAngle, float targetAngle, float minAngle, float maxAngle)
    {
        var angleDelta = Mathf.DeltaAngle(currentPlayerAngle, targetAngle);
        return currentPlayerAngle + Mathf.Clamp(angleDelta, minAngle, maxAngle);
    }
}