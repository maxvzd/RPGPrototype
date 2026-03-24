using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float speed = 3;
    [SerializeField] private float maxLookAngleUp = 60;
    [SerializeField] private float maxLookAngleDown = -60;
    [SerializeField] private float maxHorizontalLook;
    [SerializeField] private float maxHorizontalCameraTilt;
    
    private Vector3 _rotation = Vector3.zero;
    private float _targetTilt;
    private float _tiltVelocity;
    private float _zAxisTilt;

    private bool _lockCamera = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void TiltCamera(float horizontalDirection)
    {
        _targetTilt = horizontalDirection * maxHorizontalCameraTilt * -1;

        var cameraEuler = fpCamera.transform.eulerAngles;
        _zAxisTilt = Mathf.SmoothDampAngle(cameraEuler.z, _targetTilt, ref _tiltVelocity, 0.2f);
    }

    public void LockCamera()
    {
        _lockCamera = true;
    }

    public void UnlockCamera()
    {
        _lockCamera = false;
    }

    public void MoveCamera(Vector2 mouseInput)
    {
        if (_lockCamera)
        {
            mouseInput = Vector2.zero;
        }
        
        _rotation.x += -mouseInput.y * speed;
        _rotation.x = Mathf.Clamp(_rotation.x, maxLookAngleDown, maxLookAngleUp);

        var angleDiff = Mathf.Abs(fpCamera.transform.eulerAngles.y - transform.eulerAngles.y);
        
        //Limit horizontal look angle to avoid players being able to look at their own neck
        if (angleDiff < maxHorizontalLook || (angleDiff > 360 - maxHorizontalLook && angleDiff < 360))
        {
            _rotation.y += mouseInput.x * speed;
        }
        _rotation.z = _zAxisTilt;
        cameraContainer.transform.rotation = Quaternion.Euler(_rotation);
    }
}
