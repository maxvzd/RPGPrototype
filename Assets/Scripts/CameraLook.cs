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
        _rotation.y += cameraTransform.x * speed;
        _rotation.x += -cameraTransform.y * speed;

        _rotation.x = Mathf.Clamp(_rotation.x, maxLookAngleDown, maxLookAngleUp);
        fpCamera.transform.eulerAngles = _rotation;
    }
}
