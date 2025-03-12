using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private float speed = 3;
    [SerializeField] private float maxLookAngleUp = 60;
    [SerializeField] private float maxLookAngleDown = -60;
    [SerializeField] private float maxHorizontalLook;
    
    private Vector2 _rotation = Vector2.zero;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MoveCamera(Vector2 mouseInput)
    {
        _rotation.x += -mouseInput.y * speed;
        _rotation.x = Mathf.Clamp(_rotation.x, maxLookAngleDown, maxLookAngleUp);

        var angleDiff = Mathf.Abs(fpCamera.transform.eulerAngles.y - transform.eulerAngles.y);
        
        //Limit horizontal look angle to avoid players being able to look at their own neck
        if (angleDiff < maxHorizontalLook || (angleDiff > 360 - maxHorizontalLook && angleDiff < 360))
        {
            _rotation.y += mouseInput.x * speed;
        }
        fpCamera.transform.eulerAngles = _rotation;
    }
}
