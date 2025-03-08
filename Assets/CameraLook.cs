using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private float speed = 3;
    [SerializeField] private float maxLookAngleUp = 60;
    [SerializeField] private float maxLookAngleDown = -60;
    
    Vector2 _rotation = Vector2.zero;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MoveCamera(Vector2 cameraTransform)
    {
        _rotation.y += cameraTransform.x;
        _rotation.x += -cameraTransform.y;

        var rotation = _rotation * speed;
        var currentRotation = transform.eulerAngles;
        rotation.x = Mathf.Clamp(rotation.x, maxLookAngleDown, maxLookAngleUp);
        
        fpCamera.transform.eulerAngles = rotation;
        transform.eulerAngles = new Vector3(currentRotation.x, rotation.y, currentRotation.z);
    }
}
